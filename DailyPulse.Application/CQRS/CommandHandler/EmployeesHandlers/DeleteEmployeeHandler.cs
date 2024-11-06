using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Employees;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.CommandHandler.EmployeesHandlers
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, Unit>
    {
        private readonly IGenericRepository<Employee> _repository;

        public DeleteEmployeeHandler(IGenericRepository<Employee> _repository)
        {
            this._repository = _repository;
        }
        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetByIdAsync(request.EmployeeId);
            if (employee == null)
            {
                throw new KeyNotFoundException("Employee not found");
            }

            await _repository.DeleteAsync(employee, cancellationToken);
            return Unit.Value;
        }
    }
}
