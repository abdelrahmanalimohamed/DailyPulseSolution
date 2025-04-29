using DailyPulse.Application.DTO.Base;

namespace DailyPulse.Application.DTO
{
	internal sealed class SelectedEmployeeDTO : BaseDTO
	{
		public Guid? ReportToId { get; set; }
		public int Role { get; set; }
		public string Title { get; set; }
		public string Email { get; set; }
	}
}