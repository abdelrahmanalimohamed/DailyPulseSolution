﻿using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Tasks;
using DailyPulse.Application.ViewModel;
using DailyPulse.Domain.Enums;
using MediatR;
using Task = DailyPulse.Domain.Entities.Task;

namespace DailyPulse.Application.CQRS.QueriesHandler.TaskHandlers
{
    internal sealed class GetAllTasksByEmployeeIdHandler : IRequestHandler<GetAllTasksByEmployeeIdQuery, IEnumerable<TaskHeaderViewModel>>
    {
        private readonly IGenericRepository<Task> _repository;

        public GetAllTasksByEmployeeIdHandler(IGenericRepository<Task> _repository)
        {
            this._repository = _repository;
        }

        public async Task<IEnumerable<TaskHeaderViewModel>> Handle(GetAllTasksByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _repository.FindAsync(
                                  x => x.EmpId == request.EmployeeId &&
                                 !x.IsRejectedByAdmin &&
                                 (x.Status == Status.New || x.Status == Status.RequestToReAssign),
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
