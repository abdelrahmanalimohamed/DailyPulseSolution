using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Employees;
using DailyPulse.Application.DTO;
using DailyPulse.Application.ViewModel;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.EmployeeHandlers
{
    public class GetEmployeeTeamLeaderHandler : IRequestHandler<GetEmployeeTeamLeaderQuery, IEnumerable<EmployeeViewModel>>
    {
        private readonly IGenericRepository<Employee> _repository;

        public GetEmployeeTeamLeaderHandler(IGenericRepository<Employee> _repository)
        {
            this._repository = _repository;
        }
        public async Task<IEnumerable<EmployeeViewModel>> Handle(GetEmployeeTeamLeaderQuery request, CancellationToken cancellationToken)
        {
            var empTeamleads = await _repository.FindAsync(x => x.Role == EmployeeRole.TeamLeader);

            var teamLeads = empTeamleads.Select(emp => new EmployeeViewModel
            {
                Id = emp.Id,
                Name = emp.Name
            });

            return teamLeads;
        }
    }
}
