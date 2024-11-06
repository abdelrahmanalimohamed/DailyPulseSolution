using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.ScopeOfWorks;
using DailyPulse.Application.DTO;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.ScopeOfWorkHandlers
{
    public class GetScopeOfWorksHandler : IRequestHandler<GetScopeOfWorksQuery, IEnumerable<ScopeOfWorkDTO>>
    {
        private readonly IGenericRepository<ScopeOfWork> _repository;
        public GetScopeOfWorksHandler(IGenericRepository<ScopeOfWork> _repository)
        {
            this._repository = _repository;
        }
        public async Task<IEnumerable<ScopeOfWorkDTO>> Handle(GetScopeOfWorksQuery request, CancellationToken cancellationToken)
        {
            var scopeOfWork = await _repository.GetAllAsync(cancellationToken);

            var scopeOfWorksDTOs = scopeOfWork.Select(region => new ScopeOfWorkDTO
            {
                Id = region.Id,
                Name = region.Name
            });

            return scopeOfWorksDTOs;
        }
    }
}