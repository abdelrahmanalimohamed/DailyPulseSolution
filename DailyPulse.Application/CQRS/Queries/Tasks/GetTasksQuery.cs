using DailyPulse.Application.Common;
using DailyPulse.Application.ViewModel;
using DailyPulse.Domain.Common;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Tasks
{
    public class GetTasksQuery : IRequest<PagedResponse<TaskHeaderViewModel>>
    {
        public RequestParameters RequestParameters { get; set; }
    }
}