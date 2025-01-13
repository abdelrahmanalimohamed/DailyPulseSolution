using DailyPulse.Application.ViewModel;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Projects
{
	public class GetProjectsByRegionAndLocationQuery : IRequest<IEnumerable<ProjectsViewModel>>
	{
		public Guid RegionId { get; set; }

		public Guid LocationId { get; set; }
	}
}