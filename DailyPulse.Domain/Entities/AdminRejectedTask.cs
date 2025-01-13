using DailyPulse.Domain.Base;
using DailyPulse.Domain.Enums;

namespace DailyPulse.Domain.Entities
{
    public class AdminRejectedTask : BaseEntity
    {
        public Guid TaskId { get; set; }
        public Task Task { get; set; }
        public Status Status { get; set; }
        public string? ClosedComments { get; set; }
    }
}