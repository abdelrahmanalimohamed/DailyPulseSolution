﻿using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Tasks;
using DailyPulse.Application.ViewModel;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using MediatR;
using Task = DailyPulse.Domain.Entities.Task;

namespace DailyPulse.Application.CQRS.QueriesHandler.TaskHandlers
{
    public class GetWorkedTasksByEmployeeIdHandler : IRequestHandler<GetWorkedTasksByEmployeeIdQuery, IEnumerable<TaskHeaderViewModel>>
    {
        private readonly IGenericRepository<Task> _repository;

        public GetWorkedTasksByEmployeeIdHandler(IGenericRepository<Task> _repository)
        {
            this._repository = _repository;
        }
        public async Task<IEnumerable<TaskHeaderViewModel>> Handle(GetWorkedTasksByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _repository.FindAsync(
                                x => x.EmpId == request.EmployeeId &&
                               !x.IsRejectedByAdmin &&
                               (x.Status == Status.InProgress || x.Status == Status.Completed),
                          cancellationToken);

            var taskHeaderViewModel = tasks.Select(task => new TaskHeaderViewModel
            {
                Id = task.Id,
                Name = task.Name,
                Status = task.Status.ToString(),
            });

            return taskHeaderViewModel;
        }
    }
}