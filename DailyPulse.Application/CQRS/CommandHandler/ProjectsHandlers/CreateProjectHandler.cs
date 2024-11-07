using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Projects;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.CommandHandler.ProjectsHandlers
{
    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, Unit>
    {
        private readonly IGenericRepository<Project> _repository;

        public CreateProjectHandler(IGenericRepository<Project> _repository)
        {
            this._repository = _repository;
        }

        public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project
            {
                Description = request.Description,
                LocationId = request.LocationId,
                Name = request.Name,
                RegionId = request.RegionId,
                ScopeOfWorkId = request.ScopeOfWorkId,
                TeamLeadId = request.TeamLeadId
            };

            await _repository.AddAsync(project , cancellationToken);
            return Unit.Value;
        }
    }
}