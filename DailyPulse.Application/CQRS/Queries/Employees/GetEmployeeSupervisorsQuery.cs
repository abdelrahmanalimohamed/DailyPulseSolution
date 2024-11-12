using DailyPulse.Application.ViewModel;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Employees
{
    public class GetEmployeeSupervisorsQuery : IRequest<IEnumerable<EmployeeViewModel>>
    {
    }
}
