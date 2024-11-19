using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Projects;
using DailyPulse.Domain.Entities;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.CQRS.CommandHandler.ProjectsHandlers
{
    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand>
    {
        private readonly IGenericRepository<Project> _repository;

        private readonly IGenericRepository<ProjectsScopes> _projectScopeAssignmentsRepository;

        public CreateProjectHandler(
            IGenericRepository<Project> _repository ,
            IGenericRepository<ProjectsScopes> _projectScopeAssignmentsRepository)
        {
            this._repository = _repository;
            this._projectScopeAssignmentsRepository = _projectScopeAssignmentsRepository;
        }

        public async Task Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project
            {
                Description = request.Description,
                LocationId = request.LocationId,
                Name = request.Name,
                RegionId = request.RegionId
            };

            var insertedProject = await _repository.AddAsyncWithReturnEntity(project, cancellationToken);

            await InsertProjectScopes(insertedProject, request.ScopeOfWorksSelections , cancellationToken);

        }

        private async Task InsertProjectScopes(Project project , List<Guid> Scopes , CancellationToken cancellationToken)
        {
          
            foreach (var scopeId in Scopes)
            {
                var projectScopes = new ProjectsScopes
                {
                    ProjectId = project.Id,
                    ScopeOfWorkId = scopeId
                };

                await _projectScopeAssignmentsRepository.AddAsync(projectScopes, cancellationToken);
            }

        }
    }
}