using MediatR;

namespace DailyPulse.Application.CQRS.Commands.Projects
{
    public class DeleteProjectCommand : IRequest
    {
        public Guid ProjectId { get; set; }
    }
}
