using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Tasks;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.CQRS.CommandHandler.TasksHandlers
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand>
    {
        private readonly IGenericRepository<DailyPulse.Domain.Entities.Task> _repository;
        private readonly IGenericRepository<TaskStatusLogs> _taskstatusLogsrepo;

        public UpdateTaskHandler(IGenericRepository<DailyPulse.Domain.Entities.Task> _repository, 
            IGenericRepository<TaskStatusLogs> _taskstatusLogsrepo)
        {
            this._repository = _repository;
            this._taskstatusLogsrepo = _taskstatusLogsrepo;
        }
        public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _repository.GetByIdAsync(request.TaskId, cancellationToken);

            task.Name = request.TaskName;
            task.DateFrom = request.StartDate;
            task.DateTo = request.EndDate;
            task.ProjectId = request.ProjectId;

            task.Priority = Enum.TryParse(request.Priority, true, out Priority role)
                     ? role : throw new ArgumentException($"Invalid priority: {request.Priority}");

            task.EmpId = request.EmpId;

            await SaveStatusLogIfNeeded(task, cancellationToken);
            await _repository.UpdateAsync(task , cancellationToken);
        }

        private async Task SaveStatusLogIfNeeded(
            DailyPulse.Domain.Entities.Task task, 
            CancellationToken cancellationToken)
        {
            if (task.Status == Status.RequestToReAssign)
            {
                var oldStatus = task.Status;
                task.Status = Status.New;
                await SaveTaskStatusLog(task.Id, oldStatus, task.Status, cancellationToken);
            }
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

            await _taskstatusLogsrepo.AddAsync(taskStatusLogs, cancellationToken);
        }
    }
}