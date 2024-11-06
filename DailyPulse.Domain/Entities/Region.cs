using DailyPulse.Domain.Base;

namespace DailyPulse.Domain.Entities
{
    public class Region : BaseEntityWithName
    {
        public ICollection<Project> Projects { get; set; } = new List<Project>();
        public ICollection<Location> Locations { get; set; } = new List<Location>();
    }
}