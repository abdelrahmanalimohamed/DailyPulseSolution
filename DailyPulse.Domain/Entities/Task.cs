using DailyPulse.Domain.Base;
using DailyPulse.Domain.Enums;

namespace DailyPulse.Domain.Entities
{
    public class Task : BaseEntityWithName
    {
        public string DrawingId { get; set; }
        public string DrawingTitle { get; set; }
        public string FilePath { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid EmpId { get; set; } // Foreign key to Employee
        public Guid ScopeId { get; set; }  // Foreign key to Scope
        public Guid ProjectId { get; set; }  // Foreign key to Scope
        public Employee Employee { get; set; }
        public ScopeOfWork Scope { get; set; }
        public Project Project { get; set; }
        public ICollection<TaskDetail> TaskDetails { get; set; } = new List<TaskDetail>();
        public ICollection<ReAssign> ReAssigns { get; set; } = new List<ReAssign>();
    }
}
