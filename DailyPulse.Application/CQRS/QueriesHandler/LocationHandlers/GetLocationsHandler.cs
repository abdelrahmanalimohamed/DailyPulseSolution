using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Locations;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.LocationHandlers
{
    public class GetLocationsHandler : IRequestHandler<GetLocationsQuery, IEnumerable<Location>>
    {
        private readonly IGenericRepository<Location> _repository;
        public GetLocationsHandler(IGenericRepository<Location> _repository)
        {
            this._repository = _repository;
        }
        public async Task<IEnumerable<Location>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
    }
}
