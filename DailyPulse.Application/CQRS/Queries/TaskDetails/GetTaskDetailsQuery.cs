using DailyPulse.Application.ViewModel;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.TaskDetails
{
    public class GetTaskDetailsQuery : IRequest<IEnumerable<TaskDetailsViewModel>>
    {
        public Guid TaskId { get; set; }
    }
}
