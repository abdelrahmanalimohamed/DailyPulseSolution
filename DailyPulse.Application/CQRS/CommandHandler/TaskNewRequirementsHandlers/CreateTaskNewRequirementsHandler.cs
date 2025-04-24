using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.TaskNewRequirements;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.CQRS.CommandHandler.TaskNewRequirementsHandlers
{
    public class CreateTaskNewRequirementsHandler : IRequestHandler<CreateTaskNewRequirementsCommand, Unit>
    {
        private readonly IGenericRepository<TaskNewRequirements> _repository;

        private readonly IGenericRepository<DailyPulse.Domain.Entities.Task> _taskRepository;

        private readonly IGenericRepository<TaskStatusLogs> _taskstatusLogsrepo;

        public CreateTaskNewRequirementsHandler(
            IGenericRepository<TaskNewRequirements> _repository, 
            IGenericRepository<Domain.Entities.Task> taskRepository,
            IGenericRepository<TaskStatusLogs> taskstatusLogsrepo)
        {
            this._repository = _repository;
            _taskRepository = taskRepository;
            _taskstatusLogsrepo = taskstatusLogsrepo;
        }
        public async Task<Unit> Handle(CreateTaskNewRequirementsCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.TaskId  , cancellationToken);

            var oldStatus = task.Status;

            if (task.Status == Status.Pending_Approval)
            {
                task.Status = Status.InProgress;
                await _taskRepository.UpdateAsync(task , cancellationToken);
                await SaveTaskStatusLog(task.Id , oldStatus , task.Status , cancellationToken);
            }
            var taskNewRequirement = new TaskNewRequirements
            {
                TaskId = request.TaskId,
                RequirementsDetails = request.RequirementDescription,
                EstimatedWorkingHours = request.EstimatedWorkingHours,
                CreatedBy = request.CreatedBy
            };

            await _repository.AddAsync(taskNewRequirement , cancellationToken);
            return Unit.Value;
        }

        private async Task SaveTaskStatusLog(
            Guid taskId,
            Status OldStatus,
            Status NewStatus, CancellationToken cancellationToken)
        {
            var taskStatusLogs = new TaskStatusLogs
            {
                TaskId = taskId,
                OldStatus = OldStatus,
                NewStatus = NewStatus
            };

            await _taskstatusLogsrepo.AddAsync(taskStatusLogs, cancellationToken);
        }
    }
}
