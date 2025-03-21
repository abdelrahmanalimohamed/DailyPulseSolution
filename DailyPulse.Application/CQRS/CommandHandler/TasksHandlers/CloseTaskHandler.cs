using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Tasks;
using DailyPulse.Application.DTO;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.CQRS.CommandHandler.TasksHandlers
{
    public class CloseTaskHandler : IRequestHandler<CloseTaskCommand, Unit>
    {
        private readonly IGenericRepository<DailyPulse.Domain.Entities.Task> _taskrepository;

        private readonly IGenericRepository<AdminRejectedTask> _tasklogsrepo;

        private readonly IGenericRepository<TaskStatusLogs> _taskstatusLogsrepo;

        public CloseTaskHandler(
            IGenericRepository<DailyPulse.Domain.Entities.Task> _taskrepository,
            IGenericRepository<AdminRejectedTask> _tasklogsrepo,
            IGenericRepository<TaskStatusLogs> _taskstatusLogsrepo)
        {
            this._taskrepository = _taskrepository;
            this._tasklogsrepo = _tasklogsrepo;
            this._taskstatusLogsrepo = _taskstatusLogsrepo;
        }
        public async Task<Unit> Handle(CloseTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskrepository.GetByIdAsync(request.TaskId , cancellationToken);

            var oldStaus = task.Status;

			task.Status = Enum.Parse<Status>(request.Status);

			await _taskrepository.UpdateAsync(task , cancellationToken);

            await SaveToLogs(request , cancellationToken);

            SaveTaskStatusDTO saveTaskStatusDTO = new SaveTaskStatusDTO(
                task.Id, oldStaus, task.Status, request.MachineName);

			await SaveTaskStatusLog(saveTaskStatusDTO, cancellationToken);

            return Unit.Value;
        }

        private async Task SaveToLogs(CloseTaskCommand request, CancellationToken cancellationToken)
        {
            var tasklog = new AdminRejectedTask
            {
                Status = (Status)Enum.Parse(typeof(Status), request.Status),
                TaskId = request.TaskId,
                ClosedComments = request.log
            };

            await _tasklogsrepo.AddAsync(tasklog, cancellationToken);
        }
        private async Task SaveTaskStatusLog(
		    SaveTaskStatusDTO saveTaskStatusDTO ,
			CancellationToken cancellationToken)
        {
            var taskStatusLogs = new TaskStatusLogs
            {
                TaskId = saveTaskStatusDTO.taskId,
                OldStatus = saveTaskStatusDTO.oldStatus,
                NewStatus = saveTaskStatusDTO.newStatus,
                MachineName = saveTaskStatusDTO.machineName
            };

            await _taskstatusLogsrepo.AddAsync(taskStatusLogs, cancellationToken);
        }
    }
}