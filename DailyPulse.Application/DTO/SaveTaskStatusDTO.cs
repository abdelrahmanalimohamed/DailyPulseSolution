using DailyPulse.Domain.Enums;

namespace DailyPulse.Application.DTO
{
    record SaveTaskStatusDTO(
			Guid taskId,
			Status oldStatus,
			Status newStatus,
			string machineName);
}