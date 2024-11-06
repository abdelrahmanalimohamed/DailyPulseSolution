using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Locations;
using DailyPulse.Application.DTO;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.LocationHandlers
{
    public class GetLocationByIdHandler : IRequestHandler<GetLocationByIdQuery, LocationDTO>
    {
        private readonly IGenericRepository<Location> _repository;

        public GetLocationByIdHandler(IGenericRepository<Location> _repository)
        {
            this._repository = _repository;
        }
        public async Task<LocationDTO> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            var location = await _repository.GetByIdAsync(request.LocationId , cancellationToken);

            var locationDTOs = new LocationDTO
            {
                Id = location.Id,
                Name = location.Name,
            };

            return locationDTOs;
        }
    }
}
