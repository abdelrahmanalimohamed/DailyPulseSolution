using DailyPulse.Application.DTO;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Tasks
{
    public class GetTaskReportQuery : IRequest<IEnumerable<TaskReportDTO>>
    {
        public Guid TaskId { get; set; }
    }
}
