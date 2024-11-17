using MediatR;

namespace DailyPulse.Application.CQRS.Commands.Tasks
{
    public class UpdateTaskStatusByEmployeeCommand : IRequest
    {
        public Guid TaskId { get; set; }
        public string Action { get; set; }
    }
}
