using DailyPulse.Application.DTO;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.ScopeOfWorks
{
    public class GetScopeOfWorksQuery : IRequest<IEnumerable<ScopeOfWorkDTO>>
    {
    }
}
