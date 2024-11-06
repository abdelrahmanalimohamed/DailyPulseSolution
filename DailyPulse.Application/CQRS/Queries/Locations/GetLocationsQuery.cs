using DailyPulse.Application.DTO;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Locations
{
    public class GetLocationsQuery : IRequest<IEnumerable<LocationDTO>>
    {
    }
}
