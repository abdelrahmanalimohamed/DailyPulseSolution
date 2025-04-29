using DailyPulse.Application.DTO;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Employees;
public sealed class GetSelectedEmployeeQuery : IRequest<SelectedEmployeeDTO>
{
	public Guid EmployeeId { get; set; }
}