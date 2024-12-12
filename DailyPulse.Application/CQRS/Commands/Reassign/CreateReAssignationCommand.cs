using MediatR;

namespace DailyPulse.Application.CQRS.Commands.Reassign
{
    public class CreateReAssignationCommand : IRequest
    {
        public Guid EmpId { get; set; }
       
        public Guid TaskId { get; set; }

        public Guid TeamLeadId { get; set; }
    }
}
