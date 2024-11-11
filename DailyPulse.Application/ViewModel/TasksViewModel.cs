using DailyPulse.Application.ViewModel.BaseViewModel;

namespace DailyPulse.Application.ViewModel
{
    public class TasksViewModel : ViewModelBase
    {
        public string ProjectName { get; set; }

        public string Priority { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ScopeOfWork {  get; set; }

        public string Area { get; set; }

        public string DrawingNo { get; set; }

        public string DrawingTitle { get; set; }

        public string Attachement { get; set; }

        public string AssignedBy { get; set; }
    }
}
