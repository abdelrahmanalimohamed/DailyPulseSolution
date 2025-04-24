using AutoMapper;
using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Tasks;
using DailyPulse.Application.ViewModel;
using MediatR;
using System.Linq.Expressions;
using Task = DailyPulse.Domain.Entities.Task;

namespace DailyPulse.Application.CQRS.QueriesHandler.TaskHandlers;
internal sealed class GetTaskInnerDetailsHandler : IRequestHandler<GetTaskInnerDetailsQuery, IEnumerable<TaskInnerDetailsViewModel>>
{
	private readonly IGenericRepository<Task> _taskRepo;
	private readonly IMapper _mapper;
	public GetTaskInnerDetailsHandler(
		IGenericRepository<Task> taskRepo ,
		IMapper mapper)
	{
		_taskRepo = taskRepo;
		_mapper = mapper;
	}
	public async Task<IEnumerable<TaskInnerDetailsViewModel>> Handle(GetTaskInnerDetailsQuery request, CancellationToken cancellationToken)
	{
		var includes = new List<Expression<Func<Task, object>>>
				 {
					 task => task.Project , 
					 task => task.TaskTypeDetails , 
					 task => task.CreatedByEmployee
                 };

		var task = await _taskRepo.FindWithIncludeAsync(
				  x => x.Id == request.TaskId,
				  includes
			  );
		
		var taskInnerDetails = _mapper.Map<IEnumerable<TaskInnerDetailsViewModel>>(task);
		return taskInnerDetails;
	}
}
