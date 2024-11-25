using MediatR;

namespace DailyPulse.Application.CQRS.Commands.Tasks
{
    public class UpdateTaskCommand : IRequest
    {
        public Guid TaskId { get; set; }
        public string TaskName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
