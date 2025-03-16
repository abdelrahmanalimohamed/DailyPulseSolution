using DailyPulse.Domain.Base;
using DailyPulse.Domain.Enums;

namespace DailyPulse.Domain.Entities
{
    public class Task : BaseEntityWithName
    {
        public string DrawingId { get; set; }
        public string DrawingTitle { get; set; }
        public string FilePath { get; set; }
       // public string Area { get; set; }
        public string EstimatedWorkingHours { get; set; }
        public bool IsRejectedByAdmin { get; set; }
        public bool IsRejectedByEmployee { get; set; }
        public string CreatedByMachine { get; set; }
        public string? OtherTypes { get; set; }
        public Status Status { get; set; }
        public Levels Levels { get; set; }
        public Priority Priority { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid EmpId { get; set; } // Foreign key to Employee
        public Guid ProjectId { get; set; }  // Foreign key to Scope
        public Guid? TaskTypeDetailsId { get; set; }
        public Employee Employee { get; set; }
        public Project Project { get; set; }
        public TaskTypeDetails TaskTypeDetails { get; set; }
        public ICollection<TaskWorkLog> TaskDetails { get; set; } = new List<TaskWorkLog>();
        public ICollection<ReAssign> ReAssigns { get; set; } = new List<ReAssign>();
        public ICollection<EmployeeRejectedTasks> RejectedTasks { get; set; } = new List<EmployeeRejectedTasks>();

        public ICollection<TaskNewRequirements> TaskNewRequirements = new List<TaskNewRequirements>();

        public ICollection<AdminRejectedTask> TaskLogs = new List<AdminRejectedTask>();

        public ICollection<TaskStatusLogs> TaskStatusLogs = new List<TaskStatusLogs>();
    }
}