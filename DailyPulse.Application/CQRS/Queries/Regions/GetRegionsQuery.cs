using DailyPulse.Application.DTO;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Regions
{
    public class GetRegionsQuery : IRequest<IEnumerable<RegionsDTO>>
    {
    }
}
