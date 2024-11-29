using DailyPulse.Domain.Base;

namespace DailyPulse.Domain.Entities
{
    public class ScopeOfWork : BaseEntityWithName
    {
        //public ICollection<ProjectsScopes> ProjectsScopes { get; set; } = new List<ProjectsScopes>();

        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}