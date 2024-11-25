using DailyPulse.Domain.Base;
using DailyPulse.Domain.Enums;

namespace DailyPulse.Domain.Entities
{
    public class Project : BaseEntityWithName
    {
        public string Description { get; set; }
        public Guid RegionId { get; set; }
        public Guid LocationId { get; set; }
       // public Guid TeamLeadId { get; set; } // This links to an Employee with the TeamLeader role

        // Navigation properties
        public Region Region { get; set; }
        public Location Location { get; set; }
        public Trades Trade { get; set; }
       // public Employee TeamLead { get; set; }
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
       // public ICollection<ProjectsScopes> ProjectsScopes { get; set; }
    }
}
