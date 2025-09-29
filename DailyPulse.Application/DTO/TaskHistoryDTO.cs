using DailyPulse.Domain.Enums;

namespace DailyPulse.Application.DTO
{
    public class TaskHistoryDTO
    {
        public string DateAndTime { get; set; }
        public TasksStatus OldStatus { get; set; }
        public TasksStatus NewStatus { get; set; }
        public string? BackToAdminReasons { get; set; }
        public string? BackToAdminTime { get; set; }
        public string? AdminClosingReasons { get; set; }
        public string? AdminClosingTime { get; set; }
    }
}