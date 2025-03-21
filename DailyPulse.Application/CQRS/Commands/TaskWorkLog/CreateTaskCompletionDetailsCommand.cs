using MediatR;

namespace DailyPulse.Application.CQRS.Commands.TaskWorkLog
{
    public class CreateTaskCompletionDetailsCommand : IRequest
    {
        public Guid TaskId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime PauseTime { get; set; }
        public DateTime EndTime { get; set; }
        public string LogDesc { get; set; }
        public string MachineName { get; set; }
    }
}