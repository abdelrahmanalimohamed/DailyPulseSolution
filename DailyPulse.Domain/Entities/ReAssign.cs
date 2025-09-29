using DailyPulse.Domain.Base;

namespace DailyPulse.Domain.Entities
{
    public class ReAssign : BaseEntity
    {
        public Guid EmpId { get; set; }
        public Guid TeamLeadId { get; set; }
        public Guid TaskId { get; set; }
        public Task Task { get; set; }
        public Employee Employee { get; set; }
        public Employee TeamLead { get; set; }
    }
}