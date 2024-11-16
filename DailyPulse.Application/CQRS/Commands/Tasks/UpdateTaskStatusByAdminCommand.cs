using MediatR;

namespace DailyPulse.Application.CQRS.Commands.Tasks
{
    public class UpdateTaskStatusByAdminCommand : IRequest
    {
        public Guid TaskId { get; set; }

        public string Action { get; set; }
    }
}
