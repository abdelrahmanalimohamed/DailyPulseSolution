using AutoMapper;
using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Projects;
using DailyPulse.Application.ViewModel;
using DailyPulse.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace DailyPulse.Application.CQRS.QueriesHandler.ProjectHandlers;
internal sealed class GetProjectsDetailsHandler : IRequestHandler<GetProjectsDetailsQuery, IEnumerable<ProjectsDetailsViewModel>>
{
	private readonly IGenericRepository<Project> _projectRepository;
	private readonly IMapper _mapper;
	public GetProjectsDetailsHandler(
		IGenericRepository<Project> projectRepository ,
		IMapper mapper)
	{
		_projectRepository = projectRepository;
		_mapper = mapper;
	}
	public async Task<IEnumerable<ProjectsDetailsViewModel>> Handle(GetProjectsDetailsQuery request, CancellationToken cancellationToken)
	{
		var includes = new List<Expression<Func<Project, object>>>
				 {
					 project => project.Region , 
					 project => project.Location , 
					 project => project.Employee
                 };

		var projects = await _projectRepository.FindWithIncludeAsync(null , includes , cancellationToken);

		var x = _mapper.Map<IEnumerable<ProjectsDetailsViewModel>>(projects);
		return x;
	}
}