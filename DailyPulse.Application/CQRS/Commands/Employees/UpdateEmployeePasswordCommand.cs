using MediatR;

namespace DailyPulse.Application.CQRS.Commands.Employees
{
	public class UpdateEmployeePasswordCommand : IRequest<Unit>
	{
		public Guid employeeId { get; set; }

		public string password { get; set; }
	}
}