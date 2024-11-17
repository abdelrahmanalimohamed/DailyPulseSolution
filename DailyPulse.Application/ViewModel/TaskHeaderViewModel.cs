using DailyPulse.Application.ViewModel.BaseViewModel;

namespace DailyPulse.Application.ViewModel
{
    public class TaskHeaderViewModel : ViewModelBase
    {
        public string Status { get; set; }

        public string EmployeeName { get; set; }
    }
}