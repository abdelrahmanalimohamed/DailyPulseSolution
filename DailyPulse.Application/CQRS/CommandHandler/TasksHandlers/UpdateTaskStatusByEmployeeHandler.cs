using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Tasks;
using DailyPulse.Domain.Enums;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.CQRS.CommandHandler.TasksHandlers
{
    public class UpdateTaskStatusByEmployeeHandler : IRequestHandler<UpdateTaskStatusByEmployeeCommand>
    {
        private readonly IGenericRepository<DailyPulse.Domain.Entities.Task> _repository;

        public UpdateTaskStatusByEmployeeHandler(IGenericRepository<DailyPulse.Domain.Entities.Task> _repository)
        {
            this._repository = _repository;
        }
        public async Task Handle(UpdateTaskStatusByEmployeeCommand request, CancellationToken cancellationToken)
        {
            var task = await _repository.GetByIdAsync(request.TaskId)
               ?? throw new KeyNotFoundException("Task not found");

            task.Status = request.Action == "Accepted" ? Status.InProgress : Status.Rejected;

            task.IsRejectedByEmployee = task.Status == Status.Rejected;

            await _repository.UpdateAsync(task, cancellationToken);
        }
    }
}