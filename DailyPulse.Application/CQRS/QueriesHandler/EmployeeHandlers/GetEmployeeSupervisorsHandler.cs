using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Employees;
using DailyPulse.Application.ViewModel;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.EmployeeHandlers
{
    public class GetEmployeeSupervisorsHandler : IRequestHandler<GetEmployeeSupervisorsQuery, IEnumerable<EmployeeViewModel>>
    {
        private readonly IGenericRepository<Employee> _repository;

        public GetEmployeeSupervisorsHandler(IGenericRepository<Employee> _repository)
        {
            this._repository = _repository;
        }
        public async Task<IEnumerable<EmployeeViewModel>> Handle(GetEmployeeSupervisorsQuery request, CancellationToken cancellationToken)
        {
            var empSuperVisors = await _repository.FindAsync
                (x => x.Role == EmployeeRole.TeamLeader 
                 || x.Role == EmployeeRole.Senior 
                 || x.Role == EmployeeRole.Admin);

            var superVisors = empSuperVisors.Select(emp => new EmployeeViewModel
            {
                Id = emp.Id,
                Name = emp.Name
            });

            return superVisors;
        }
    }
}
