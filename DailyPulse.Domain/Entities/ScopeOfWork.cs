using DailyPulse.Domain.Base;

namespace DailyPulse.Domain.Entities
{
    public class ScopeOfWork : BaseEntityWithName
    {
        public ICollection<Project> Projects { get; set; } = new List<Project>();

        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
