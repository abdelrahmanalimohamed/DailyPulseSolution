namespace DailyPulse.Application.ViewModel
{
    public class TaskHistoryViewModel
    {
        public string DateAndTime { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string BackToAdminReasons { get; set; }
        public string BackToAdminTime { get; set; }
        public string AdminClosingReasons { get; set; }
        public string AdminClosingTime { get; set; }
    }
}