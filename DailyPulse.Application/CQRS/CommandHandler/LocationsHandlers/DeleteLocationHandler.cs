using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Locations;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.CommandHandler.LocationsHandlers
{
    public class DeleteLocationHandler : IRequestHandler<DeleteLocationCommand, Unit>
    {
        private readonly IGenericRepository<Location> _repository;
        public DeleteLocationHandler(IGenericRepository<Location> repository)
        {
            this._repository = repository;
        }
        public async Task<Unit> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _repository.GetByIdAsync(request.LocationId);
            if (location == null)
            {
                throw new KeyNotFoundException("Location not found");
            }

            await _repository.DeleteAsync(location, cancellationToken);
            return Unit.Value;
        }
    }
}
