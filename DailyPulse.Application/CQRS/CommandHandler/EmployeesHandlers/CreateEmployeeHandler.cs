using System.Text.RegularExpressions;
using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Employees;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.CQRS.CommandHandler.EmployeesHandlers
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand>
    {
        private readonly IGenericRepository<Employee> _repository;

        public CreateEmployeeHandler(IGenericRepository<Employee> _repository)
        {
            this._repository = _repository;
        }
        public async Task Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            string grade = Regex.Replace(request.Jobgrade, @"\s+", "");

            var employee = new Employee
            {
                Name = request.Name,
                Title = request.Title,
                username = request.Email,
                password = hashedPassword,
                Role = Enum.TryParse(grade, true, out EmployeeRole role)
                     ? role : throw new ArgumentException($"Invalid job grade: {request.Jobgrade}"),
                ReportToId = request.ReportTo,
                IsAdmin = false 
            };

            await _repository.AddAsync(employee, cancellationToken);
        }
    }
}
