using DailyPulse.Application.ViewModel;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Tasks
{
    public class GetTasksByEmployeeIdQuery : IRequest<IEnumerable<TaskHeaderViewModel>>
    {
        public Guid EmployeeId { get; set; }
    }
}
