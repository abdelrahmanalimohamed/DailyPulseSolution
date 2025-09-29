using DailyPulse.Domain.Base;
using DailyPulse.Domain.Enums;

namespace DailyPulse.Domain.Entities;
public class ProjectTrades : BaseEntity
{
	public Guid ProjectId { get; set; }
	public Project Project { get; set; }
	public Trades Trades { get; set; }
}