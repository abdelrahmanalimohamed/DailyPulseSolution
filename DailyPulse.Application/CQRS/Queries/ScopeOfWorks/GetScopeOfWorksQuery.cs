using DailyPulse.Application.DTO;
using DailyPulse.Application.ViewModel;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.ScopeOfWorks
{
    public class GetScopeOfWorksQuery : IRequest<IEnumerable<ScopeOfWorkViewModel>>
    {
    }
}
