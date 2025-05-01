using MediatR;

namespace DailyPulse.Application.CQRS.Commands.Employees
{
    public class UpdateEmployeeCommand : IRequest
    {
        public Guid employeeId { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public int role { get; set; }
        public string email { get; set; }
        public Guid reportToId { get; set; }
    }
}