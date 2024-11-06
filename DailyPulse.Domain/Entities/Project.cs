using DailyPulse.Domain.Base;

namespace DailyPulse.Domain.Entities
{
    public class Project : BaseEntityWithName
    {
        public string Description { get; set; }
        public string Drawings  { get; set; }
        public Guid ScopeOfWorkId { get; set; }
        public Guid RegionId { get; set; }
        public Guid LocationId { get; set; }
        public Guid TeamLeadId { get; set; } // This links to an Employee with the TeamLeader role

        // Navigation properties
        public ScopeOfWork ScopeOfWork { get; set; }
        public Region Region { get; set; }
        public Location Location { get; set; }
        public Employee TeamLead { get; set; }
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
