using DailyPulse.Domain.Base;
using DailyPulse.Domain.Enums;

namespace DailyPulse.Domain.Entities
{
    public class Project : BaseEntityWithName
    {
        public string Description { get; set; }
        public string BuildingNo { get; set; }
        public string ProjectNo { get; set; }
        public Guid RegionId { get; set; }
        public Guid LocationId { get; set; }
        public Guid EmployeeId { get; set; }
       // public Guid TeamLeadId { get; set; } // This links to an Employee with the TeamLeader role

        // Navigation properties
        public Region Region { get; set; }
        public Location Location { get; set; }
        public Employee Employee { get; set; }
        public Treats Trade { get; set; }
       // public Employee TeamLead { get; set; }
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
       // public ICollection<ProjectsScopes> ProjectsScopes { get; set; }
    }
}