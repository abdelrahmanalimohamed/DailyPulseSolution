using MediatR;

namespace DailyPulse.Application.CQRS.Commands.Tasks
{
    public class UpdateTaskRejectionByEmployeeCommand : IRequest
    {
        public Guid TaskId { get; set; }
        public string Reasons { get; set; }
    }
}
