using DailyPulse.Application.ViewModel;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Tasks
{
    public class GetTaskByIdQuery : IRequest<IEnumerable<TasksViewModel>>
    {
        public Guid TaskId { get; set; }
    }
}
