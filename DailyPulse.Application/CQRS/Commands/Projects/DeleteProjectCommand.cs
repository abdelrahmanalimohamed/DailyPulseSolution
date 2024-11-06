using MediatR;

namespace DailyPulse.Application.CQRS.Commands.Projects
{
    public class DeleteProjectCommand : IRequest<Unit>
    {
        public Guid ProjectId { get; set; }
    }
}
