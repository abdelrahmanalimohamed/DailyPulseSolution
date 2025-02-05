using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Employees;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.CommandHandler.EmployeesHandlers;
public class UpdateEmployeePasswordHandler : IRequestHandler<UpdateEmployeePasswordCommand, Unit>
{
	private readonly IGenericRepository<Employee> _userRepository;
	public UpdateEmployeePasswordHandler(IGenericRepository<Employee> _userRepository)
	{
		this._userRepository = _userRepository;
	}
	public async Task<Unit> Handle(UpdateEmployeePasswordCommand request, CancellationToken cancellationToken)
	{
		var employee = await _userRepository.GetByIdAsync(request.employeeId);
		if (employee is null)
		{
			throw new Exception("Employee not found");
		}

		var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.password);
		employee.Password = hashedPassword;
		await _userRepository.UpdateAsync(employee);
		return Unit.Value;
	}
}