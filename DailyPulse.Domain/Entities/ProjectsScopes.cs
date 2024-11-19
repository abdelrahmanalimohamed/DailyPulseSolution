namespace DailyPulse.Domain.Entities
{
    public class ProjectsScopes
    {
        public Guid ProjectId { get; set; }

        public Project Project { get; set; }

        public Guid ScopeOfWorkId { get; set; }

        public ScopeOfWork ScopeOfWork { get; set; }
    }
}
