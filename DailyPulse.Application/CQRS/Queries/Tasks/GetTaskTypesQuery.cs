using DailyPulse.Application.DTO;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Tasks
{
    public class GetTaskTypesQuery : IRequest<IEnumerable<TaskTypeDTO>>
    {
    }
}