using DailyPulse.Domain.Enums;

namespace DailyPulse.Application.DTO
{
    record SaveTaskStatusDTO(
			Guid taskId,
			TasksStatus oldStatus,
			TasksStatus newStatus,
			string machineName);
}