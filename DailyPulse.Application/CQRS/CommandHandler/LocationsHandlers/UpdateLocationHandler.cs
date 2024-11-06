using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Locations;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.CommandHandler.LocationsHandlers
{
    public class UpdateLocationHandler : IRequestHandler<UpdateLocationCommand, Unit>
    {
        private readonly IGenericRepository<Location> _repository;
        public UpdateLocationHandler(IGenericRepository<Location> repository)
        {
            this._repository = repository;
        }
        public async Task<Unit> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _repository.GetByIdAsync(request.LocationId);
            if (location == null)
            {
                throw new KeyNotFoundException("Location not found");
            }

            location.Name = request.Name;
            location.RegionId = request.RegionId;

            await _repository.UpdateAsync(location, cancellationToken);
            return Unit.Value;
        }
    }
}
