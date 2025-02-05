using DailyPulse.Application.ViewModel.BaseViewModel;

namespace DailyPulse.Application.ViewModel
{
    public class TaskHeaderViewModel : ViewModelBase
    {
        public string Status { get; set; }

        public string EmployeeName { get; set; }

        public string CreatedDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Priority { get; set; }

        public string Overdue { get; set; }

        public string ProjectName { get; set; }
    }
}