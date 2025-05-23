﻿using System.Linq.Expressions;
using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Tasks;
using DailyPulse.Application.ViewModel;
using MediatR;
using Task = DailyPulse.Domain.Entities.Task;

namespace DailyPulse.Application.CQRS.QueriesHandler.TaskHandlers
{
    public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, IEnumerable<TasksViewModel>>
    {
        private readonly IGenericRepository<Task> _repository;

        public GetTaskByIdHandler(IGenericRepository<Task> _repository)
        {
            this._repository = _repository;
        }

        public async Task<IEnumerable<TasksViewModel>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Task, object>>>
                 {
                     task => task.Project ,
                    // task => task.Scope 
                 };

            var tasks = await _repository.FindWithIncludeAsync(
                        x => x.Id == request.TaskId,
                        includes
                    );

            var taskViewModel = tasks.Select(task => new TasksViewModel
            {
                DrawingNo = task.DrawingId,
                Id = task.Id,
                Attachement = task.FilePath,
                StartDate = task.DateFrom,
                EndDate = task.DateTo,
                DrawingTitle = task.DrawingTitle,
                Name = task.Name,
                Priority = task.Priority.ToString(),
                Status = task.Status.ToString(),
               // Area = task.Area,
                ProjectName = task.Project.Name,
               // ScopeOfWork = task.Scope.Name , 
                ProjectId = task.ProjectId ,
                EmpId = task.EmpId ,
                EstimatedWorkingHours = task.EstimatedWorkingHours
            });

            return taskViewModel;
        }
    }
}
