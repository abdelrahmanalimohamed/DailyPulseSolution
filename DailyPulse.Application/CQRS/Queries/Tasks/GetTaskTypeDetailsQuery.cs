using DailyPulse.Application.DTO;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Tasks
{
    public class GetTaskTypeDetailsQuery : IRequest<IEnumerable<TaskTypesDetailsDTO>>
    {
        public Guid tasktypeId { get; set; }
    }
}