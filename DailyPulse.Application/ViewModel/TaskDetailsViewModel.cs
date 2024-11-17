namespace DailyPulse.Application.ViewModel
{
    public class TaskDetailsViewModel
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime PauseTime { get; set; }

        public string Log { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
