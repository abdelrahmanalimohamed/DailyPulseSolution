using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Tasks;
using DailyPulse.Application.ViewModel;
using DailyPulse.Domain.Common;
using DailyPulse.Domain.Enums;
using MediatR;
using System.Linq.Expressions;
using Task = DailyPulse.Domain.Entities.Task;

namespace DailyPulse.Application.CQRS.QueriesHandler.TaskHandlers;

internal sealed class GetTasksHandler : IRequestHandler<GetTasksQuery, PagedResponse<TaskHeaderViewModel>>
{
    private readonly IGenericRepository<Task> _repository;
    public GetTasksHandler(IGenericRepository<Task> _repository)
    {
        this._repository = _repository;
    }
    public async Task<PagedResponse<TaskHeaderViewModel>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<Task, object>>>
             {
                 task => task.Employee, 
                 task => task.Project , 
                 task => task.CreatedByEmployee
             };

		var tasks = await _repository.FindWithIncludePaginated(
			predicate: null,
			includes: includes,
            requestParameters: request.RequestParameters,
			cancellationToken: cancellationToken
		);


		var todayDate = DateTime.Now;

        var mappedTasks = tasks.Select(task => new TaskHeaderViewModel
        {
            Id = task.Id,
            Name = task.Name,
            Status = task.Status.ToString(),
            EmployeeName = task.Employee.Name,
            StartDate = task.DateFrom,
            EndDate = task.DateTo,
            CreatedDate = task.CreatedDate.ToString("dd-MM-yyyy"),
            Priority = task.Priority.ToString(),
            ProjectName = task.Project.Name,
            CreatedBy  = task.CreatedByEmployee?.Name,
            Overdue = todayDate > task.DateTo && task.Status.ToString() == Status.InProgress.ToString()
              ? $"{(todayDate - task.DateTo).Days} Days"
              : ""
        });
		var metaData = new MetaData
		{
			CurrentPage = tasks.MetaData.CurrentPage,
			TotalPages = tasks.MetaData.TotalPages,
			PageSize = tasks.MetaData.PageSize,
			TotalCount = tasks.MetaData.TotalCount
		};
        var x = new PagedResponse<TaskHeaderViewModel> { Items = mappedTasks.ToList(), MetaData = metaData };
	    
        return x;
		//return taskHeaderViewModel;
    }
}
