using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Tasks;
using DailyPulse.Domain.Entities;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.CQRS.CommandHandler.TasksHandlers
{
    public class CreateTaskDetailsHandler : IRequestHandler<CreateTaskDetailsCommand>
    {
        private readonly IGenericRepository<TaskDetail> _repository;

        public CreateTaskDetailsHandler(IGenericRepository<TaskDetail> _repository)
        {
            this._repository = _repository;
        }
        public async Task Handle(CreateTaskDetailsCommand request, CancellationToken cancellationToken)
        {
            var taskDetails = new TaskDetail
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
