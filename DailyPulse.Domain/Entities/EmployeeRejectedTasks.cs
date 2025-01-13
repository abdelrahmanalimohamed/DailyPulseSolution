using DailyPulse.Domain.Base;

namespace DailyPulse.Domain.Entities
{
    public class EmployeeRejectedTasks : BaseEntity
    {
        public Guid TaskId { get; set; }
        public Task Task { get; set; }
        public Guid EmpId { get; set; }
        public Employee Employee { get; set; }
        public string RejectionReasons { get; set; }
    }
}