using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Regions;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.RegionHandlers
{
    public class GerRegionsHandler : IRequestHandler<GetRegionsQuery, IEnumerable<Region>>
    {
        private readonly IGenericRepository<Region> _repository;

        public GerRegionsHandler(IGenericRepository<Region> _repository)
        {
           this._repository = _repository;
        }
        public async Task<IEnumerable<Region>> Handle(GetRegionsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
    }
}
