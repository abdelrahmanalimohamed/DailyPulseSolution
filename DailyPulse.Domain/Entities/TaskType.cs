using DailyPulse.Domain.Base;

namespace DailyPulse.Domain.Entities
{
    public class TaskType : BaseEntityWithName
    {
        public List<Task> Task = new List<Task>();

        public List<TaskTypeDetails> TaskTypeDetails = new List<TaskTypeDetails>();
    }
}