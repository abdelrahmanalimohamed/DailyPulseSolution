using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Tasks;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.CQRS.CommandHandler.TasksHandlers
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand>
    {
        private readonly IGenericRepository<DailyPulse.Domain.Entities.Task> _repository;

        public UpdateTaskHandler(IGenericRepository<DailyPulse.Domain.Entities.Task> _repository)
        {
            this._repository = _repository;
        }
        public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _repository.GetByIdAsync(request.TaskId, cancellationToken);

            task.Name = request.TaskName;
            task.DateFrom = request.StartDate;
            task.DateTo = request.EndDate;

            await _repository.UpdateAsync(task , cancellationToken);
        }
    }
}