using MediatR;

namespace DailyPulse.Application.CQRS.Commands.Tasks
{
    public class UpdateTaskStatusByEmployeeCommand : IRequest
    {
        public Guid TaskId { get; set; }
        public string Action { get; set; }
        public string? Comment { get; set; }
        public string MachineName { get; set; }
    }
}
