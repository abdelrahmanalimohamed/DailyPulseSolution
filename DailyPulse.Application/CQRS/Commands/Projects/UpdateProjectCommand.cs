using MediatR;

namespace DailyPulse.Application.CQRS.Commands.Projects
{
    public class UpdateProjectCommand : IRequest
    {
        public Guid projectId { get; set; }
        public string projectName { get; set; }
        public string description { get; set; }
        public string projectNo { get; set; }
        public Guid LocationId { get; set; }
        public Guid RegionId { get; set; }
    }
}