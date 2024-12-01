using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Tasks;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.CQRS.CommandHandler.TasksHandlers
{
    public class CloseTaskHandler : IRequestHandler<CloseTaskCommand, Unit>
    {
        private readonly IGenericRepository<DailyPulse.Domain.Entities.Task> _taskrepository;

        private readonly IGenericRepository<TaskLogs> _tasklogsrepo;

        private readonly IGenericRepository<TaskStatusLogs> _taskstatusLogsrepo;

        public CloseTaskHandler(
            IGenericRepository<DailyPulse.Domain.Entities.Task> _repository ,
            IGenericRepository<TaskLogs> logsrepo,
            IGenericRepository<TaskStatusLogs> taskstatusLogsrepo)
        {
            this._taskrepository = _repository;
            this._tasklogsrepo = logsrepo;
            _taskstatusLogsrepo = taskstatusLogsrepo;
        }
        public async Task<Unit> Handle(CloseTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskrepository.GetByIdAsync(request.TaskId , cancellationToken);

            var oldStaus = task.Status;

            task.Status = (Status)Enum.Parse(typeof(Status), request.Status);

            await _taskrepository.UpdateAsync(task , cancellationToken);

            await SaveToLogs(request , cancellationToken);
            await SaveTaskStatusLog(task.Id , oldStaus , task.Status , cancellationToken);

            return Unit.Value;
        }

        private async Task SaveToLogs(CloseTaskCommand request, CancellationToken cancellationToken)
        {
            var tasklog = new TaskLogs
            {
                Status = (Status)Enum.Parse(typeof(Status), request.Status),
                TaskId = request.TaskId,
                ClosedComments = request.log
            };

            await _tasklogsrepo.AddAsync(tasklog, cancellationToken);
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