using DailyPulse.Application.ViewModel.BaseViewModel;

namespace DailyPulse.Application.ViewModel;

internal sealed class CompletedTaskViewModel : ViewModelBase
{
	public string Description { get; set; }
	public string Status { get; set; }
	public string ProjectName { get; set; }
	public string DrawingNo { get; set; }
	public string DrawingTitle { get; set; }
	public List<string> Comments { get; set; }
	public string ActualWorkingHours { get; set; }
	public string EstimatedWorkingHours { get; set; }
	public string? CreatedBy { get; set; }
	public string Priority { get; set; }
	public string CreatedDate { get; set; }
}