using DailyPulse.Application.DTO;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Locations
{
    public class GetLocationByIdQuery : IRequest<LocationDTO>
    {
        public Guid LocationId { get; set; }
    }
}
