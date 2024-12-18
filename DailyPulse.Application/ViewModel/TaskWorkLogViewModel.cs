namespace DailyPulse.Application.ViewModel
{
    public class TaskWorkLogViewModel
    {
        public int TaskWorkLogID { get; set; }
        public string Name { get; set; }
        public string Start_DateTime { get; set; }
        public string? Pause_DateTime { get; set; }
        public string? Total_Working_Hours { get; set; }
        public string? WorkLogDuration { get; set; }
        public string? LogDesc { get; set; }
        public string? Remaining_Time { get; set; }
    }
}