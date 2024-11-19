using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.ScopeOfWorks;
using DailyPulse.Application.ViewModel;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.ScopeOfWorkHandlers
{
    public class GetScopeOfWorksHandler : IRequestHandler<GetScopeOfWorksQuery, IEnumerable<ScopeOfWorkViewModel>>
    {
        private readonly IGenericRepository<ScopeOfWork> _repository;
        public GetScopeOfWorksHandler(IGenericRepository<ScopeOfWork> _repository)
        {
            this._repository = _repository;
        }
        public async Task<IEnumerable<ScopeOfWorkViewModel>> Handle(GetScopeOfWorksQuery request, CancellationToken cancellationToken)
        {
            var scopeOfWork = await _repository.GetAllAsync(cancellationToken);

            var scopeOfWorksViewModel = scopeOfWork.Select(region => new ScopeOfWorkViewModel
            {
                Id = region.Id,
                Name = region.Name
            });

            return scopeOfWorksViewModel;
        }
    }
}