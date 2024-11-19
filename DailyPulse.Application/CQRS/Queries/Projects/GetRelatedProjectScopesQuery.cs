using DailyPulse.Application.ViewModel;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Projects
{
    public class GetRelatedProjectScopesQuery: IRequest<IEnumerable<ScopeOfWorkViewModel>>
    {
        public Guid ProjectId { get; set; }
    }
}
