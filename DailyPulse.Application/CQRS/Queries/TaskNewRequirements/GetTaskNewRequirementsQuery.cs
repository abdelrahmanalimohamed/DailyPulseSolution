using DailyPulse.Application.ViewModel;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.TaskNewRequirements;
public sealed class GetTaskNewRequirementsQuery : IRequest<IEnumerable<TaskNewRequirementsViewModel>>
{
	public Guid TaskId { get; set; }
}