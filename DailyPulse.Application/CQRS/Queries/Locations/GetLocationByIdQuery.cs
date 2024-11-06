using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Locations
{
    public class GetLocationByIdQuery : IRequest<Location>
    {
        public Guid LocationId { get; set; }
    }
}
