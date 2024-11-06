using MediatR;

namespace DailyPulse.Application.CQRS.Commands.Employees
{
    public class DeleteEmployeeCommand : IRequest<Unit>
    {
        public Guid EmployeeId { get; set; }
    }
}
