using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Projects;
using DailyPulse.Application.DTO;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.ProjectHandlers
{
    public class GetProjectsHandler : IRequestHandler<GetProjectsQuery, IEnumerable<ProjectDTO>>
    {
        private readonly IGenericRepository<Project> _repository;

        public GetProjectsHandler(IGenericRepository<Project> _repositor)
        {
           this._repository = _repositor;
        }
        public async Task<IEnumerable<ProjectDTO>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _repository.GetAllAsync(cancellationToken);

            var projectsDto = projects.Select(project => new ProjectDTO
            {
                Id = project.Id,
                Name = project.Name
            });

            return projectsDto;
        }
    }
}