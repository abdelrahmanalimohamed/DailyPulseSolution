using DailyPulse.Domain.Enums;
using MediatR;

namespace DailyPulse.Application.CQRS.Commands.Employees
{
    public class UpdateEmployeeCommand : IRequest<Unit>
    {
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        public int Jobgrade { get; set; }

        public string Email { get; set; }
        
        public string Password { get; set; }
        public EmployeeRole Role { get; set; }
        public Guid ReportTo { get; set; }
    }
}
