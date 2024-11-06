using DailyPulse.Application.DTO;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Locations
{
    public class GetLocationsByRegionIdQuery : IRequest<IEnumerable<LocationDTO>>
    {
        public Guid RegionId { get; set; }
    }
}
