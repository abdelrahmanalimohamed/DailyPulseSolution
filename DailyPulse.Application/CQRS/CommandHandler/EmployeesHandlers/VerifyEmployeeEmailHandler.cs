using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Employees;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.CommandHandler.EmployeesHandlers
{
	public class VerifyEmployeeEmailHandler : IRequestHandler<VerifyEmployeeEmailCommand, bool>
	{
		private readonly IGenericRepository<Employee> _employeeRepo;
		public VerifyEmployeeEmailHandler(IGenericRepository<Employee> _employeeRepo)
		{
			this._employeeRepo = _employeeRepo;
		}
		public async Task<bool> Handle(VerifyEmployeeEmailCommand request, CancellationToken cancellationToken)
		{
			var employee = await _employeeRepo.GetByIdAsync(request.employeeId);

			employee.IsEmailVerified = true;

			await _employeeRepo.UpdateAsync(employee, cancellationToken);

			return true;
		}
	}
}