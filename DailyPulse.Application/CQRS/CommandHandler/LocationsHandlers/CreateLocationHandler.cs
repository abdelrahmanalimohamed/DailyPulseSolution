using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Locations;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.CommandHandler.LocationsHandlers
{
    public class CreateLocationHandler : IRequestHandler<CreateLocationCommand, Unit>
    {
        private readonly IGenericRepository<Location> _repository;

        public CreateLocationHandler(IGenericRepository<Location> _repository)
        {
            this._repository = _repository;
        }

        public async Task<Unit> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = new Location
            {
                Name = request.Name,
                RegionId = request.RegionId
            };

            await _repository.AddAsync(location , cancellationToken);
            return Unit.Value;
        }
    }
}
