using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Tasks;
using DailyPulse.Domain.Enums;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.CQRS.CommandHandler.TasksHandlers
{
    public class UpdateTaskStatusByAdminHandler : IRequestHandler<UpdateTaskStatusByAdminCommand>
    {
        private readonly IGenericRepository<DailyPulse.Domain.Entities.Task> _repository;
        public UpdateTaskStatusByAdminHandler(IGenericRepository<DailyPulse.Domain.Entities.Task> _repository)
        {
            this._repository = _repository;
        }

        public async Task Handle(UpdateTaskStatusByAdminCommand request, CancellationToken cancellationToken)
        {
            var task = await _repository.GetByIdAsync(request.TaskId)
           ?? throw new KeyNotFoundException("Task not found");

            task.Status = request.Action == "Accepted" ? Status.InProgress : Status.Canceled;

            task.IsRejectedByAdmin = task.Status == Status.Canceled;

            await _repository.UpdateAsync(task, cancellationToken);
        }
    }
}