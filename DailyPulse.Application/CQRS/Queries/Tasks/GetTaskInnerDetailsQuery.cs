using DailyPulse.Application.ViewModel;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Tasks;
public class GetTaskInnerDetailsQuery : IRequest<IEnumerable<TaskInnerDetailsViewModel>>
{
	public Guid TaskId { get; set; }
}
