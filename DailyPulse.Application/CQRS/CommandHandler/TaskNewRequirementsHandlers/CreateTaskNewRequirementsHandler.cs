using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.TaskNewRequirements;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using MediatR;

namespace DailyPulse.Application.CQRS.CommandHandler.TaskNewRequirementsHandlers
{
    public class CreateTaskNewRequirementsHandler : IRequestHandler<CreateTaskNewRequirementsCommand, Unit>
    {
        private readonly IGenericRepository<TaskNewRequirements> _repository;

        private readonly IGenericRepository<DailyPulse.Domain.Entities.Task> _taskRepository;

        public CreateTaskNewRequirementsHandler(
            IGenericRepository<TaskNewRequirements> _repository, 
            IGenericRepository<Domain.Entities.Task> taskRepository)
        {
            this._repository = _repository;
            _taskRepository = taskRepository;
        }
        public async Task<Unit> Handle(CreateTaskNewRequirementsCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.TaskId  , cancellationToken);

            if (task.Status == Status.Pending_Approval)
            {
                task.Status = Status.InProgress;
                await _taskRepository.UpdateAsync(task , cancellationToken);
            }
            var taskNewRequirement = new TaskNewRequirements
            {
                TaskId = request.TaskId,
                RequirementsDetails = request.RequirementDescription,
                EstimatedWorkingHours = request.EstimatedWorkingHours,
            };

            await _repository.AddAsync(taskNewRequirement , cancellationToken);
            return Unit.Value;
        }
    }
}
