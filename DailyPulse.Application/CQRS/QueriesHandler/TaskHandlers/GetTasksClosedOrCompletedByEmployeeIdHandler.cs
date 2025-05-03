using AutoMapper;
using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Tasks;
using DailyPulse.Application.ViewModel;
using DailyPulse.Domain.Enums;
using MediatR;
using System.Linq.Expressions;
using Task = DailyPulse.Domain.Entities.Task;

namespace DailyPulse.Application.CQRS.QueriesHandler.TaskHandlers;
internal sealed class GetTasksClosedOrCompletedByEmployeeIdHandler : 
	IRequestHandler<GetTasksClosedOrCompletedByEmployeeIdQuery, IEnumerable<CompletedTaskViewModel>>
{
	private readonly IGenericRepository<Task> _taskRepository;
	private readonly IMapper _mapper;
	public GetTasksClosedOrCompletedByEmployeeIdHandler(
		IGenericRepository<Task> taskRepository ,
		IMapper mapper)
	{
		_taskRepository = taskRepository;
		_mapper = mapper;
	}
	public async Task<IEnumerable<CompletedTaskViewModel>> Handle(GetTasksClosedOrCompletedByEmployeeIdQuery request, CancellationToken cancellationToken)
	{
		var includes = new List<Expression<Func<Task, object>>>
			 {
				 task => task.Project ,
				 task => task.TaskNewRequirements ,
				 task => task.CreatedByEmployee , 
				 task => task.TaskLogs , 
				 task => task.TaskDetails
			 };

		var closedORCompletedTasks = await _taskRepository.FindWithIncludeAsync(
				 predicate: x => x.EmpId == request.EmployeeID 
				 && (x.Status == Status.Closed || x.Status == Status.Completed) ,
				  includes: includes , 
				  cancellationToken
			  );

		var totalDuration = closedORCompletedTasks
				.SelectMany(task => task.TaskDetails)
				.Aggregate(TimeSpan.Zero, (sum, detail) => sum + (detail.PauseTime - detail.StartTime));

		int totalHours = (int)totalDuration.TotalHours;
		int totalMinutes = totalDuration.Minutes;

		var viewModel = closedORCompletedTasks.Select(task => new CompletedTaskViewModel
		{
			Id = task.Id,
			Name = task.Name,
			Status = task.Status.ToString(),
			CreatedDate = task.CreatedDate.ToString("dd-MM-yyyy"),
			Priority = task.Priority.ToString(),
			ProjectName = task.Project.Name,
			CreatedBy  = task.CreatedByEmployee?.Name,
			EstimatedWorkingHours = $" {task.EstimatedWorkingHours} Hours ",
			Comments = task.TaskLogs.Select(log => log.ClosedComments).ToList(),
			ActualWorkingHours = $"{totalHours} Hourse and {totalMinutes} Minutes" , 
			Description = task.TaskDescription , 
			DrawingNo = task.DrawingId , 
			DrawingTitle = task.DrawingTitle
		});

		return viewModel;
	}
}
