using DailyPulse.Domain.Base;

namespace DailyPulse.Domain.Entities
{
    public class TaskTypeDetails : BaseEntityWithName
    {
        public Guid TaskTypeId { get; set; }
        public TaskType TaskType { get; set; }

        public List<Task> Tasks = new List<Task>();
    }
}