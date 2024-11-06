using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Projects
{
    public class GetProjectByIdQuery : IRequest<Project>
    {
        public Guid ProjectId { get; set; }
    }
}
