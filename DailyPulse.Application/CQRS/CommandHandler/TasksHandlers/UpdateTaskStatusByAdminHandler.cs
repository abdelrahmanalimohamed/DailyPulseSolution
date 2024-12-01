using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Tasks;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.CQRS.CommandHandler.TasksHandlers
{
    public class UpdateTaskStatusByAdminHandler : IRequestHandler<UpdateTaskStatusByAdminCommand>
    {
        private readonly IGenericRepository<DailyPulse.Domain.Entities.Task> _repository;

        private readonly IGenericRepository<TaskStatusLogs> _taskstatusLogsrepo;
        public UpdateTaskStatusByAdminHandler(
            IGenericRepository<DailyPulse.Domain.Entities.Task> _repository ,
            IGenericRepository<TaskStatusLogs> _taskstatusLogsrepo)
        {
            this._repository = _repository;
            this._taskstatusLogsrepo = _taskstatusLogsrepo;
        }

        public async Task Handle(UpdateTaskStatusByAdminCommand request, CancellationToken cancellationToken)
        {
            var task = await _repository.GetByIdAsync(request.TaskId)
           ?? throw new KeyNotFoundException("Task not found");

            var oldStatus = task.Status;

            task.Status = request.Action == "Accepted" ? Status.InProgress : Status.Canceled;

            task.IsRejectedByAdmin = task.Status == Status.Canceled;

            await _repository.UpdateAsync(task, cancellationToken);

            await SaveTaskStatusLog(task.Id, oldStatus, task.Status, cancellationToken);
        }

        private async Task SaveTaskStatusLog(
            Guid taskId, 
            Status OldStatus, 
            Status NewStatus , CancellationToken cancellationToken) {
            var taskStatusLogs = new TaskStatusLogs
            {
                TaskId = taskId,
                OldStatus = OldStatus,
                NewStatus = NewStatus
            };

            await _taskstatusLogsrepo.AddAsync(taskStatusLogs , cancellationToken);
        }
    }
}