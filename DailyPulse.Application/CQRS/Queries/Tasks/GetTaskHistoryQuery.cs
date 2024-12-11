using DailyPulse.Application.ViewModel;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Tasks
{
    public class GetTaskHistoryQuery : IRequest<IEnumerable<TaskHistoryViewModel>>
    {
        public Guid TaskId { get; set; }
    }
}