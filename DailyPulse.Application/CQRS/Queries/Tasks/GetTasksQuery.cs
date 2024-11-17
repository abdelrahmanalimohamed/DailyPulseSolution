using DailyPulse.Application.ViewModel;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Tasks
{
    public class GetTasksQuery : IRequest<IEnumerable<TaskHeaderViewModel>>
    {
    }
}