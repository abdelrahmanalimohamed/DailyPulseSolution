using DailyPulse.Domain.Enums;

namespace DailyPulse.Application.DTO
{
    public class TaskWorkLogDTO
    {
        public int TaskWorkLogID { get; set; }
        public string Name { get; set; }
        public string Start_DateTime { get; set; }
        public string? Pause_DateTime { get; set; }
        public string? LogDesc { get; set; }
        public string? Total_Working_Hours { get; set; }
        public string? WorkLogDuration { get; set; }
    }
}