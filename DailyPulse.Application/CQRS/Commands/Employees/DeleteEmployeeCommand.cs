using MediatR;

namespace DailyPulse.Application.CQRS.Commands.Employees
{
    public class DeleteEmployeeCommand : IRequest
    {
        public Guid EmployeeId { get; set; }
    }
}
