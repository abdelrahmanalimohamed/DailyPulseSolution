using MediatR;

namespace DailyPulse.Application.CQRS.Commands.Locations
{
    public class UpdateLocationCommand : IRequest<Unit>
    {
        public Guid LocationId { get; set; }
        public string Name { get; set; }
        public Guid RegionId { get; set; }
    }
}
