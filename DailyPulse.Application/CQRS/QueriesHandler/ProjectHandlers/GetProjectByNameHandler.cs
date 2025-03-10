using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Projects;
using DailyPulse.Application.DTO;
using DailyPulse.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace DailyPulse.Application.CQRS.QueriesHandler.ProjectHandlers
{
	internal class GetProjectByNameHandler : IRequestHandler<GetProjectByNameQuery, IEnumerable<ProjectByNameDTO>>
	{
		private readonly IGenericRepository<Project> _repository;
		public GetProjectByNameHandler(IGenericRepository<Project> _repository)
		{
			this._repository = _repository;
		}
		public async Task<IEnumerable<ProjectByNameDTO>> Handle(GetProjectByNameQuery request, CancellationToken cancellationToken)
		{
			var includes = new List<Expression<Func<Project, object>>>
				 {
					project => project.Region ,
					project => project.Location
				 };

			var projects = await _repository.FindWithIncludeAsync(
					x => x.Name == request.projectName, includes);

			var projectsDto = projects.Select(project => new ProjectByNameDTO
			(project.Name , 
			project.Region.Name , 
			project.Location.Name , 
			DateOnly.FromDateTime(project.CreatedDate).ToString()));

			return projectsDto;
		}
	}
}
