using DailyPulse.Application.DTO;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Projects
{
    public class GetProjectByNameQuery : IRequest<IEnumerable<ProjectByNameDTO>>
    {
        public string projectName { get; set; }
    }
}
