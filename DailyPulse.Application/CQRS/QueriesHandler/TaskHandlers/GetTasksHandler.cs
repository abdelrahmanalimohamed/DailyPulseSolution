﻿using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Tasks;
using DailyPulse.Application.ViewModel;
using DailyPulse.Domain.Enums;
using MediatR;
using System.Linq.Expressions;
using Task = DailyPulse.Domain.Entities.Task;

namespace DailyPulse.Application.CQRS.QueriesHandler.TaskHandlers;

internal sealed class GetTasksHandler : IRequestHandler<GetTasksQuery, IEnumerable<TaskHeaderViewModel>>
{
    private readonly IGenericRepository<Task> _repository;
    public GetTasksHandler(IGenericRepository<Task> _repository)
    {
        this._repository = _repository;
    }
    public async Task<IEnumerable<TaskHeaderViewModel>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<Task, object>>>
             {
                 task => task.Employee, 
                 task => task.Project , 
                 task => task.CreatedByEmployee
             };

		var tasks = await _repository.FindWithIncludeAsync(
			predicate: null,
			includes: includes,
			cancellationToken: cancellationToken
		);


		var todayDate = DateTime.Now;

        var taskHeaderViewModel = tasks.Select(task => new TaskHeaderViewModel
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

        return taskHeaderViewModel;
    }
}
