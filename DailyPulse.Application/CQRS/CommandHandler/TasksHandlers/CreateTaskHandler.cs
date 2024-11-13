using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Tasks;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using MediatR;
using Task = System.Threading.Tasks.Task;


namespace DailyPulse.Application.CQRS.CommandHandler.TasksHandlers
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand>
    {
        private readonly IGenericRepository<DailyPulse.Domain.Entities.Task> _repository;

        public CreateTaskHandler(IGenericRepository<DailyPulse.Domain.Entities.Task> _repository)
        {
            this._repository = _repository;
        }
        public async Task Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new DailyPulse.Domain.Entities.Task
            {
                Name = request.TaskName,
                Area = request.Area,
                DateFrom = request.DateFrom,
                DateTo = request.DateTo,
                DrawingId = request.DrawingNo,
                EmpId = request.EmployeeId,
                FilePath = request.file,
                ProjectId = request.ProjectId,
                Status = Status.New,
                ScopeId = request.ScopeId,
                Priority = Enum.TryParse(request.Priority, true, out Priority role)
                     ? role : throw new ArgumentException($"Invalid priority: {request.Priority}"),
                DrawingTitle = request.DrawingTitle,
            };

            await _repository.AddAsync(task, cancellationToken);
        }
    }
}
