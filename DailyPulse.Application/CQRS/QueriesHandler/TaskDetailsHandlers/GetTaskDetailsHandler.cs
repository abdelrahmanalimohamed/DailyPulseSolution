using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.TaskDetails;
using DailyPulse.Application.ViewModel;
using DailyPulse.Domain.Entities;
using MediatR;
using Task = DailyPulse.Domain.Entities.Task;

namespace DailyPulse.Application.CQRS.QueriesHandler.TaskDetailsHandlers
{
    public class GetTaskDetailsHandler : IRequestHandler<GetTaskDetailsQuery, IEnumerable<TaskDetailsViewModel>>
    {
        private readonly IGenericRepository<TaskWorkLog> _repository;
        private readonly IGenericRepository<Task> _taskRepository;
        public GetTaskDetailsHandler(
            IGenericRepository<TaskWorkLog> repository ,
			IGenericRepository<Task> taskRepository)
        {
            _repository = repository;
            _taskRepository = taskRepository;
        }
        public async Task<IEnumerable<TaskDetailsViewModel>> Handle(GetTaskDetailsQuery request, CancellationToken cancellationToken)
        {
            var taskDetails = await _repository.FindAsync(x => x.TaskId == request.TaskId , cancellationToken);
            var task = await _taskRepository.GetFirstOrDefault(x => x.Id == request.TaskId, cancellationToken);

            var totalHours = double.Parse(task.EstimatedWorkingHours);

			double totalWorkedHours = taskDetails
				.Sum(details => (details.PauseTime - details.StartTime).TotalHours);

			double remainingHours = totalHours - totalWorkedHours;

			int remainingHoursInt = (int)remainingHours;
			int remainingMinutes = (int)((remainingHours - remainingHoursInt) * 60);

			var taskDetailsViewModel = taskDetails.OrderBy(x => x.StartTime)
			.Select(details =>
					{
						return new TaskDetailsViewModel
						{
							Id = details.Id,
							StartTime = details.StartTime,
							EndTime = details.EndTime,
							Log = details.LogDesc,
							PauseTime = details.PauseTime,
							RemaningHours = $"{remainingHoursInt} Hours and {remainingMinutes} Minutes "
						};
					});

			return taskDetailsViewModel;
        }
    }
}
