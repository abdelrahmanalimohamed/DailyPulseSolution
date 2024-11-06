using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Employees;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.CommandHandler.EmployeesHandlers
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, Unit>
    {
        private readonly IGenericRepository<Employee> _repository;

        public UpdateEmployeeHandler(IGenericRepository<Employee> _repository)
        {
            this._repository = _repository;
        }
        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the employee to update
            var employee = await _repository.GetByIdAsync(request.EmployeeId, cancellationToken);
            if (employee == null)
            {
                throw new Exception("Employee not found"); 
            }

            employee.Name = request.Name ?? employee.Name;
            employee.Title = request.Title ?? employee.Title;
            employee.username = request.Email ?? employee.username; // Assuming Email is the username
            employee.Role = request.Role; // Update role
            employee.ReportToId = request.ReportTo;

            if (!string.IsNullOrEmpty(request.Password))
            {
                employee.password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            }

            await _repository.UpdateAsync(employee, cancellationToken);
            return Unit.Value;
        }
    }
}
