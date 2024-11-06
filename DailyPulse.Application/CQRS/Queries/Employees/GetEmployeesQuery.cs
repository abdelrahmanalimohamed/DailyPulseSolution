using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Employees
{
    public class GetEmployeesQuery : IRequest<IEnumerable<Employee>>
    {
    }
}
