using DailyPulse.Application.ViewModel;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Projects;

public sealed class GetProjectsDetailsQuery : IRequest<IEnumerable<ProjectsDetailsViewModel>>
{
}