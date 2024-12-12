using MediatR;

namespace DailyPulse.Application.CQRS.Commands.TaskNewRequirements
{
    public class CreateTaskNewRequirementsCommand : IRequest<Unit>
    {
        public Guid TaskId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string RequirementDescription { get; set; }
        
        public string EstimatedWorkingHours { get; set; }
    }
}
