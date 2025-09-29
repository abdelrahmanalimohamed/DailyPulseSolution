using DailyPulse.Domain.Base;
using DailyPulse.Domain.Enums;
using System.Text.RegularExpressions;

namespace DailyPulse.Domain.Entities
{
    public class Project : BaseEntityWithName
    {
        public string Description { get; set; }
        public string ProjectNo { get; set; }
        public Guid RegionId { get; set; }
        public Guid LocationId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid? ProfitCenterId { get; set; }

		// Navigation properties
		public Region Region { get; set; }
        public Location Location { get; set; }
        public Employee Employee { get; set; }
		public ProfitCenter? ProfitCenter { get; set; }
		public ProjectStatus Status { get; set; }
        public ProjectType ProjectType { get; set; }
		public Trades Trade { get; set; }
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
		private readonly List<ProjectTrades> _items = new();
		public ICollection<ProjectTrades> ProjectTrades => _items.AsReadOnly();
		public void AddTrades(string tradeId)
        {
			var cleanedTrade = Regex.Replace(tradeId, @"\s+", "");

			if (Enum.TryParse(cleanedTrade, true, out Trades trade))
			{
				_items.Add(new ProjectTrades
				{
					ProjectId = this.Id,
					Trades = trade
				});
			}
			else
			{
				throw new Exception($"Invalid trade: {tradeId}");
			}
		}
	}
}