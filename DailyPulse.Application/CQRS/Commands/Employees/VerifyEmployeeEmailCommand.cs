using MediatR;

namespace DailyPulse.Application.CQRS.Commands.Employees
{
	public class VerifyEmployeeEmailCommand : IRequest<bool>
	{
		public Guid employeeId { get; set; }
	}
}