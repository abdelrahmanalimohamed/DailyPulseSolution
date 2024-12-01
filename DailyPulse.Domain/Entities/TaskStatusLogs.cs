using DailyPulse.Domain.Base;
using DailyPulse.Domain.Enums;

namespace DailyPulse.Domain.Entities
{
    public class TaskStatusLogs : BaseEntity
    {
        public Guid TaskId { get; set; }

        public Task Task { get; set; }

        public Status OldStatus { get; set; }

        public Status NewStatus { get; set; }
    }
}