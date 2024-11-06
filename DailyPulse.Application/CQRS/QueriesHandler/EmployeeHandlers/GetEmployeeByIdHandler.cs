using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Employees;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.EmployeeHandlers
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, Employee>
    {
        private readonly IGenericRepository<Employee> _repository;

        public GetEmployeeByIdHandler(IGenericRepository<Employee> _repository)
        {
            this._repository = _repository;
        }
        public async Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.EmployeeId);
        }
    }
}
