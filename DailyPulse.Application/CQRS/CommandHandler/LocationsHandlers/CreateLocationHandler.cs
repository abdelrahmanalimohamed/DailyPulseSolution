using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Locations;
using DailyPulse.Domain.Entities;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.CQRS.CommandHandler.LocationsHandlers
{
    public class CreateLocationHandler : IRequestHandler<CreateLocationCommand>
    {
        private readonly IGenericRepository<Location> _repository;

        public CreateLocationHandler(IGenericRepository<Location> _repository)
        {
            this._repository = _repository;
        }

        public async Task Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {

            var location = new Location
            {
                Name = request.Name,
                RegionId = request.RegionId
            };

            await _repository.AddAsync(location, cancellationToken);
        }
    }
}