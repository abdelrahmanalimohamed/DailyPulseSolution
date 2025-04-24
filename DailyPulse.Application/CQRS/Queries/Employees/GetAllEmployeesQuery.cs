using DailyPulse.Application.ViewModel;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Employees;
public sealed class GetAllEmployeesQuery : IRequest<IEnumerable<AllEmployeesViewModel>>
{
}