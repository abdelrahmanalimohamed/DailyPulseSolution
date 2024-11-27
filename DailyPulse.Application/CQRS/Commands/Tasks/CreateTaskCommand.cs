using MediatR;

namespace DailyPulse.Application.CQRS.Commands.Tasks
{
    public class CreateTaskCommand : IRequest
    {
        public string TaskName { get; set; }

        public string Area { get; set; }

        public string DrawingNo { get; set; }

        public string DrawingTitle { get; set; }

        public string EstimatedHours { get; set; }

        public string file { get; set; }

        public string Priority { get; set; }
        
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public Guid ProjectId { get; set; }

        public Guid EmployeeId { get; set; }

        public Guid ScopeId { get; set; }
    }
}
