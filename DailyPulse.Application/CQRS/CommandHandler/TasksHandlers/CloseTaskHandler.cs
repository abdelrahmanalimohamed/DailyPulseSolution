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

        public CloseTaskHandler(
            IGenericRepository<DailyPulse.Domain.Entities.Task> _repository ,
            IGenericRepository<TaskLogs> logsrepo)
        {
            this._taskrepository = _repository;
            this._tasklogsrepo = logsrepo;
        }
        public async Task<Unit> Handle(CloseTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskrepository.GetByIdAsync(request.TaskId , cancellationToken);

            task.Status = (Status)Enum.Parse(typeof(Status), request.Status);

            await _taskrepository.UpdateAsync(task , cancellationToken);

            await SaveToLogs(request , cancellationToken);

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
    }
}
