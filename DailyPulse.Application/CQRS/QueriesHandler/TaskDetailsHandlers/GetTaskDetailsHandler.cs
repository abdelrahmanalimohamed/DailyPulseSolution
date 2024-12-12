using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.TaskDetails;
using DailyPulse.Application.ViewModel;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.TaskDetailsHandlers
{
    public class GetTaskDetailsHandler : IRequestHandler<GetTaskDetailsQuery, IEnumerable<TaskDetailsViewModel>>
    {
        private readonly IGenericRepository<TaskWorkLog> _repository;

        public GetTaskDetailsHandler(IGenericRepository<TaskWorkLog> _repository)
        {
            this._repository = _repository;
        }
        public async Task<IEnumerable<TaskDetailsViewModel>> Handle(GetTaskDetailsQuery request, CancellationToken cancellationToken)
        {
            var taskDetails = await _repository.FindAsync(x => x.TaskId == request.TaskId , cancellationToken);

            var taskDetailsViewModel = taskDetails.OrderBy(x => x.StartTime)
                .Select(details => new TaskDetailsViewModel
            {
                Id = details.Id,
                StartTime = details.StartTime,
                EndTime = details.EndTime,
                Log = details.LogDesc , 
                PauseTime = details.PauseTime,
            });

            return taskDetailsViewModel;
        }
    }
}
