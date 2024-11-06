using MediatR;

namespace DailyPulse.Application.CQRS.Commands.Locations
{
    public class CreateLocationCommand : IRequest<Unit>
    {
        public string Name { get; set; }

        public Guid RegionId { get; set; }
    }
}
