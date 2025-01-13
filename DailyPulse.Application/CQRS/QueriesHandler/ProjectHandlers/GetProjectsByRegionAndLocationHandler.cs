using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Projects;
using DailyPulse.Application.ViewModel;
using DailyPulse.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPulse.Application.CQRS.QueriesHandler.ProjectHandlers
{
	public class GetProjectsByRegionAndLocationHandler : IRequestHandler<GetProjectsByRegionAndLocationQuery, IEnumerable<ProjectsViewModel>>
	{
		private readonly IGenericRepository<Project> _repository;

		public GetProjectsByRegionAndLocationHandler(IGenericRepository<Project> _repository)
		{
			this._repository = _repository;
		}
		public async Task<IEnumerable<ProjectsViewModel>> Handle(GetProjectsByRegionAndLocationQuery request, CancellationToken cancellationToken)
		{
			var result = await _repository.FindAsync(x => x.RegionId == request.RegionId && 
															x.LocationId == request.LocationId , cancellationToken);

			var projectViewModel = result.Select(project => new ProjectsViewModel
			{
				Id = project.Id,
				Name = project.Name
			});

			return projectViewModel;
		}
	}
}
