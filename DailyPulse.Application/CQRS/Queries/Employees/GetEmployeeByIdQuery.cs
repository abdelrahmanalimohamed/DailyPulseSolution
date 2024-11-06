using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Employees
{
    public class GetEmployeeByIdQuery : IRequest<Employee>
    {
        public Guid EmployeeId { get; set; }
    }
}