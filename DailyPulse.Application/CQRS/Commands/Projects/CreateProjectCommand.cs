using MediatR;

namespace DailyPulse.Application.CQRS.Commands.Projects
{
    public class CreateProjectCommand : IRequest<Unit>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid ScopeOfWorkId { get; set; }

        public Guid LocationId { get; set; }

        public Guid RegionId { get; set; }

        public Guid TeamLeadId { get; set; }
    }
}
