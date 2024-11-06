using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Locations;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.LocationHandlers
{
    public class GetLocationByRegionIdHandler : IRequestHandler<GetLocationsByRegionIdQuery, IEnumerable<Location>>
    {
        private readonly IGenericRepository<Location> _repository;

        public GetLocationByRegionIdHandler(IGenericRepository<Location> _repository)
        {
            this._repository = _repository;
        }
        public async Task<IEnumerable<Location>> Handle(GetLocationsByRegionIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.FindAsync(x=> x.RegionId == request.RegionId);
        }
    }
}
