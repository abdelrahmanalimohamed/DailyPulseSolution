using DailyPulse.Domain.Base;
using DailyPulse.Domain.Enums;

namespace DailyPulse.Domain.Entities
{
    public class TaskStatusLogs : BaseEntity
    {
        public Guid TaskId { get; set; }
		public string MachineName { get; set; }
		public Task Task { get; set; }
        public TasksStatus OldStatus { get; set; }
        public TasksStatus NewStatus { get; set; }
    }
}