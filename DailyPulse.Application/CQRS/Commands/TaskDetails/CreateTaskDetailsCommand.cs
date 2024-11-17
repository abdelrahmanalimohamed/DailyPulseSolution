using MediatR;

namespace DailyPulse.Application.CQRS.Commands.TaskDetails
{
    public class CreateTaskDetailsCommand : IRequest
    {
        public Guid TaskId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime PauseTime { get; set; }
        public string LogDesc { get; set; }
    }
}
