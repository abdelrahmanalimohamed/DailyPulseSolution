using DailyPulse.Domain.Enums;

namespace DailyPulse.Application.DTO
{
    public class TaskReportDTO
    {
        public string Name { get; set; }
        public string ProjectName { get; set; }
        public string? EstimatedWorkingHours { get; set; }
        public string? Total_Working_Hours { get; set; }
        public string? Time_Difference { get; set; }
        public string? LogDesc { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? PauseTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? OldValue { get; set; }
        public int? NewValue { get; set; }
    }
}