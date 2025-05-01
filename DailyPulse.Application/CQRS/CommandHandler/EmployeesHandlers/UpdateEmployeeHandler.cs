using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Employees;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.CQRS.CommandHandler.EmployeesHandlers;
internal sealed class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand>
{
    private readonly IGenericRepository<Employee> _repository;

    public UpdateEmployeeHandler(IGenericRepository<Employee> _repository)
    {
        this._repository = _repository;
    }
    public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _repository.GetByIdAsync(request.employeeId, cancellationToken);
        if (employee == null)
        {
            throw new Exception("Employee not found"); 
        }

        employee.Name = request.name ?? employee.Name;
        employee.Title = request.title ?? employee.Title;
        employee.Email = request.email ?? employee.Email;
        employee.ReportToId = request.reportToId;
        employee.Role = (EmployeeRole)request.role;

        await _repository.UpdateAsync(employee, cancellationToken);
    }
}
