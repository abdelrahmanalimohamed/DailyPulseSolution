using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Locations
{
    public class GetLocationsByRegionIdQuery : IRequest<IEnumerable<Location>>
    {
        public Guid RegionId { get; set; }
    }
}
