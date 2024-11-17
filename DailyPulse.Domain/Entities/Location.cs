using DailyPulse.Domain.Base;

namespace DailyPulse.Domain.Entities
{
    public class Location : BaseEntityWithName
    {
        public Guid RegionId { get; set; }
        public Region Region { get; set; }
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}