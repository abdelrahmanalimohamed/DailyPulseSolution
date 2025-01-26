using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Employees;
using DailyPulse.Application.DTO;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.EmployeeHandlers
{
    public class GetEmployeesHandler : IRequestHandler<GetEmployeesQuery ,IEnumerable<EmployeesDTO>>
    {
        private readonly IGenericRepository<Employee> _repository;

        public GetEmployeesHandler(IGenericRepository<Employee> _repository)
        {
            this._repository = _repository;
        }

        public async Task<IEnumerable<EmployeesDTO>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees =  await _repository.FindAsync(x => x.IsAdmin == false , cancellationToken);
            var empDto = employees.Select(emp => new EmployeesDTO
            {
                Id = emp.Id,
                Name = emp.Name
            });

            return empDto;
        }
    }
}