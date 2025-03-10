using MediatR;

namespace DailyPulse.Application.CQRS.Commands.Tasks
{
    public class CloseTaskCommand : IRequest<Unit>
    {
        public Guid TaskId { get; set; }
        public string Status {  get; set; }
        public string log {  get; set; }
        public string MachineName { get; set; }
	}
}