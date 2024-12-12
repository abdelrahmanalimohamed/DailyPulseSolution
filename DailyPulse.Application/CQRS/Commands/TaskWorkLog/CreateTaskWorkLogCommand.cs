using MediatR;

namespace DailyPulse.Application.CQRS.Commands.TaskWorkLog
{
    public class CreateTaskWorkLogCommand : IRequest
    {
        public Guid TaskId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime PauseTime { get; set; }
        public string LogDesc { get; set; }
    }
}
