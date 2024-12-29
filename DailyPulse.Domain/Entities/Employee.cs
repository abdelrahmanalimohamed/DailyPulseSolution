using DailyPulse.Domain.Base;
using DailyPulse.Domain.Enums;

namespace DailyPulse.Domain.Entities
{
    public class Employee : BaseEntityWithName
    {
        public string Title { get; set; }

        public string username { get; set; }

        public string password { get; set; }
        public EmployeeRole Role { get; set; } // Enum for role or grade
        public bool IsAdmin { get; set; }
        // Self-referencing relationship for reporting
        public Guid? ReportToId { get; set; } // Nullable if an employee does not have a supervisor
        public Employee ReportTo { get; set; } // Navigation property for the supervisor
      //  public ICollection<Project> Projects { get; set; } = new List<Project>();
        public ICollection<Employee> DirectReports { get; set; } = new List<Employee>();
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
       // public ICollection<TaskDetail> TaskDetails { get; set; } = new List<TaskDetail>();
        public ICollection<ReAssign> ReAssigns { get; set; } = new List<ReAssign>();

        public ICollection<RejectedTasks> RejectedTasks { get; set; } = new List<RejectedTasks>();
        public ICollection<TaskLogs> NewAssignedTasks { get; set; } = new List<TaskLogs>();
        public ICollection<TaskLogs> OldAssignedTasks { get; set; } = new List<TaskLogs>();
    }
}