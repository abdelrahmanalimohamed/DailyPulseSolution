using DailyPulse.Application.ViewModel;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Tasks
{
	public class GetTasksClosedOrCompletedByEmployeeIdQuery : IRequest<IEnumerable<CompletedTaskViewModel>>
	{
		public Guid EmployeeID { get; set; }
	}
}
