using DailyPulse.Application.ViewModel.BaseViewModel;

namespace DailyPulse.Application.ViewModel;

internal sealed class AllEmployeesViewModel : ViewModelBase
{
	public string Role { get; set; }
	public string Email { get; set; }
	public string ReportingTo { get; set; }
	public string Title { get; set; }
}
