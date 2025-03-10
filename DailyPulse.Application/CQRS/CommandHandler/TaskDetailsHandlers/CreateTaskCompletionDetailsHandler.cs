using DailyPulse.Application.Abstraction;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using MediatR;
using Task = System.Threading.Tasks.Task;
using DailyPulse.Application.CQRS.Commands.TaskWorkLog;

namespace DailyPulse.Application.CQRS.CommandHandler.TaskDetailsHandlers
{
    public class CreateTaskCompletionDetailsHandler : IRequestHandler<CreateTaskCompletionDetailsCommand>
    {
        private readonly IGenericRepository<TaskWorkLog> _repository;

        private readonly IGenericRepository<DailyPulse.Domain.Entities.Task> _taskRepository;

        private readonly IGenericRepository<TaskStatusLogs> _taskStatusLogsRepository;
        public CreateTaskCompletionDetailsHandler(
            IGenericRepository<TaskWorkLog> _repository ,
            IGenericRepository<DailyPulse.Domain.Entities.Task> _taskRepository,
            IGenericRepository<TaskStatusLogs> _taskStatusLogsRepository)
        {
            this._repository = _repository;
            this._taskRepository = _taskRepository;
            this._taskStatusLogsRepository = _taskStatusLogsRepository;
        }
        public async Task Handle(CreateTaskCompletionDetailsCommand request, CancellationToken cancellationToken)
        {
            var taskDetails = new TaskWorkLog
            {
                TaskId = request.TaskId,
                StartTime = request.StartTime,
                PauseTime = request.PauseTime,
                LogDesc = request.LogDesc,
                EndTime = request.EndTime
            };

            await _repository.AddAsync(taskDetails, cancellationToken);

            await UpdateTaskStatusAsync(request.TaskId, cancellationToken);
        }

        private async Task UpdateTaskStatusAsync(Guid taskId, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(taskId, cancellationToken);

            if (task == null)
            {
                throw new KeyNotFoundException($"Task with ID {taskId} not found.");
            }


            await SaveTaskStatusLog(taskId, task.Status, Status.Pending_Approval, cancellationToken);

            task.Status = Status.Pending_Approval ;
            await _taskRepository.UpdateAsync(task, cancellationToken);
        }

        private async Task SaveTaskStatusLog(
          Guid taskId,
          Status OldStatus,
          Status NewStatus,
          CancellationToken cancellationToken)
        {
            var taskStatusLogs = new TaskStatusLogs
            {
                TaskId = taskId,
                OldStatus = OldStatus,
                NewStatus = NewStatus
            };

            await _taskStatusLogsRepository.AddAsync(taskStatusLogs, cancellationToken);
        }
    }
}