using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Employees;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.EmployeeHandlers
{
    public class GetEmployeesHandler : IRequestHandler<GetEmployeesQuery ,IEnumerable<Employee>>
    {
        private readonly IGenericRepository<Employee> _repository;

        public GetEmployeesHandler(IGenericRepository<Employee> _repository)
        {
            this._repository = _repository;
        }

        public async Task<IEnumerable<Employee>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}