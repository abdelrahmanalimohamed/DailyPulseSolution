using DailyPulse.Domain.Base;

namespace DailyPulse.Domain.Entities;
public class ProfitCenter : BaseEntity
{
	public string ProfitCenterNumber { get; set; }
	public string ProfitCenterDescription { get; set; }
	public ICollection<Project> Projects { get; set; } = new List<Project>();
}
