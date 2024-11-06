using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Locations;
using DailyPulse.Application.DTO;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.LocationHandlers
{
    public class GetLocationByRegionIdHandler : IRequestHandler<GetLocationsByRegionIdQuery, IEnumerable<LocationDTO>>
    {
        private readonly IGenericRepository<Location> _repository;

        public GetLocationByRegionIdHandler(IGenericRepository<Location> _repository)
        {
            this._repository = _repository;
        }

        public async Task<IEnumerable<LocationDTO>> Handle(GetLocationsByRegionIdQuery request, CancellationToken cancellationToken)
        {
            var locations = await _repository.FindAsync(x => x.RegionId == request.RegionId, cancellationToken);

            var locationDTOs = locations.Select(location => new LocationDTO
            {
                Id = location.Id,
                Name = location.Name
            });

            return locationDTOs;
        }
    }
}