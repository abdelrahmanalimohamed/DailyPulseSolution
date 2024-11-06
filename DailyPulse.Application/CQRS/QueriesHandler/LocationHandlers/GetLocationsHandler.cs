using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Locations;
using DailyPulse.Application.DTO;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.LocationHandlers
{
    public class GetLocationsHandler : IRequestHandler<GetLocationsQuery, IEnumerable<LocationDTO>>
    {
        private readonly IGenericRepository<Location> _repository;
        public GetLocationsHandler(IGenericRepository<Location> _repository)
        {
            this._repository = _repository;
        }
        public async Task<IEnumerable<LocationDTO>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
        {
            var locations = await _repository.GetAllAsync(cancellationToken);

            var locationDTOs = locations.Select(location => new LocationDTO
            {
                Id = location.Id,
                Name = location.Name
            });

            return locationDTOs;
        }
    }
}
