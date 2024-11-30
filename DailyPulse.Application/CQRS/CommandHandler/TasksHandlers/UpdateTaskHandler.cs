using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Tasks;
using DailyPulse.Domain.Enums;
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
            task.ProjectId = request.ProjectId;

            task.Priority = Enum.TryParse(request.Priority, true, out Priority role)
                     ? role : throw new ArgumentException($"Invalid priority: {request.Priority}");

            task.EmpId = request.EmpId;
           
            await _repository.UpdateAsync(task , cancellationToken);
        }
    }
}