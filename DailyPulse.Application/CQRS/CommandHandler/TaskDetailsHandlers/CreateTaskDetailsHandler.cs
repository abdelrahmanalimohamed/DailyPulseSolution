using DailyPulse.Application.Abstraction;
using DailyPulse.Domain.Entities;
using MediatR;
using Task = System.Threading.Tasks.Task;
using DailyPulse.Application.CQRS.Commands.TaskWorkLog;

namespace DailyPulse.Application.CQRS.CommandHandler.TaskDetailsHandlers
{
    public class CreateTaskDetailsHandler : IRequestHandler<CreateTaskWorkLogCommand>
    {
        private readonly IGenericRepository<TaskWorkLog> _repository;

        public CreateTaskDetailsHandler(IGenericRepository<TaskWorkLog> _repository)
        {
            this._repository = _repository;
        }
        public async Task Handle(CreateTaskWorkLogCommand request, CancellationToken cancellationToken)
        {
            var taskDetails = new TaskWorkLog
            {
                TaskId = request.TaskId,
                StartTime = request.StartTime,
                PauseTime = request.PauseTime,
                LogDesc = request.LogDesc,
            };

            await _repository.AddAsync(taskDetails, cancellationToken);
        }
    }
}
