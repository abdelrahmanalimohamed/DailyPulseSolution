using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Locations;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.LocationHandlers
{
    public class GetLocationByIdHandler : IRequestHandler<GetLocationByIdQuery, Location>
    {
        private readonly IGenericRepository<Location> _repository;

        public GetLocationByIdHandler(IGenericRepository<Location> _repository)
        {
            this._repository = _repository;
        }
        public async Task<Location> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.LocationId , cancellationToken);
        }
    }
}
