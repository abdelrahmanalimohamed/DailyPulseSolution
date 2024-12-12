using DailyPulse.Application.ViewModel;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Tasks
{
    public class GetTaskWorkLogQuery : IRequest<IEnumerable<TaskWorkLogViewModel>>
    {
        public Guid TaskId { get; set; }
    }
}