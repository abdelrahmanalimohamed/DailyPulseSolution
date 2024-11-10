using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Locations;
using DailyPulse.Domain.Entities;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.CQRS.CommandHandler.LocationsHandlers
{
    public class DeleteLocationHandler : IRequestHandler<DeleteLocationCommand>
    {
        private readonly IGenericRepository<Location> _repository;
        public DeleteLocationHandler(IGenericRepository<Location> repository)
        {
            this._repository = repository;
        }
        public async Task Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _repository.GetByIdAsync(request.LocationId);
            if (location == null)
            {
                throw new KeyNotFoundException("Location not found");
            }

            await _repository.DeleteAsync(location, cancellationToken);
        }
    }
}
