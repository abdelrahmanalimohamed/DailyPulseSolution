using MediatR;

namespace DailyPulse.Application.CQRS.Commands.Employees
{
    public class CreateEmployeeCommand : IRequest
    {
        public string Name { get; set; }
        public string Title { get; set; }

        public string Jobgrade { get; set; }

        public string Email { get; set; }

        // Default password set
        public string Password { get; set; } = "123456789";
        public Guid ReportTo { get; set; }
    }
}
