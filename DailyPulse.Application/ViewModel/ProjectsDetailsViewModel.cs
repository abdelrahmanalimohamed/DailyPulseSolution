using DailyPulse.Application.ViewModel.BaseViewModel;

namespace DailyPulse.Application.ViewModel;
public sealed class ProjectsDetailsViewModel : ViewModelBase
{
	public string Region { get; set; }
	public string Location { get; set; }
	public string CreatedAt { get; set; }
	public string CreatedBy { get; set; }
	public string BuildingNo { get; set; }
	public string ProjectNumber { get; set; }
	public string Description { get; set; }
}
