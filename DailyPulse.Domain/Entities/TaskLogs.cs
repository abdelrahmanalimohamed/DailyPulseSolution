using DailyPulse.Domain.Base;
using DailyPulse.Domain.Enums;

namespace DailyPulse.Domain.Entities
{
    public class TaskLogs : BaseEntity
    {
        public Guid TaskId { get; set; }
        public Task Task { get; set; }
        public Status Status { get; set; }
        public Guid OldAssignedEmp {  get; set; }
        public Employee OldEmployee { get; set; }
        public Guid NewAssignedEmp { get; set; }
        public Employee NewEmployee { get; set; }
        public string? ClosedComments { get; set; }
        public string? NewRequirements { get; set; }
        public string? RejectReasons { get; set; }

    }
}