using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Locations;
using DailyPulse.Application.Extensions;
using DailyPulse.Domain.Entities;
using MediatR;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;
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
			var normalizedName = request.Name.RemoveWhitespace();

			var existingLocation = await _repository.GetFirstOrDefault(
						loc => loc.Name.Trim().ToLower() == normalizedName.ToLower(),
					cancellationToken);

			if (existingLocation != null)
			{
				throw new DuplicateNameException("A location with the same name already exists.");
			}

			var location = new Location
			{
				Name = request.Name.Trim(),
				RegionId = request.RegionId
			};

			await _repository.AddAsync(location, cancellationToken);
		}
	}
}