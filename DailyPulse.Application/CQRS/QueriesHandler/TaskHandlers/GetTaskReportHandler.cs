using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Tasks;
using DailyPulse.Application.DTO;
using DailyPulse.Domain.Enums;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.TaskHandlers
{
    public class GetTaskReportHandler : IRequestHandler<GetTaskReportQuery, IEnumerable<TaskReportDTO>>
    {
        public readonly IGenericRepository<TaskReportDTO> _repository;
        public GetTaskReportHandler(IGenericRepository<TaskReportDTO> _repository)
        {
            this._repository = _repository;
        }
        public async Task<IEnumerable<TaskReportDTO>> Handle(GetTaskReportQuery request, CancellationToken cancellationToken)
        {
            var results = await _repository.CallStoredProc("GetTaskReport", new object[] { request.TaskId }, cancellationToken);

            var taskReportDto = results.Select(task => new TaskReportDTO
            {
                LogDesc = task.LogDesc,
                EndTime = task.EndTime,
                EstimatedWorkingHours = task.EstimatedWorkingHours,
                Name = task.Name,
                NewValue = Enum.GetName(typeof(Status), task.NewValue ?? 0),
               // OldValue = Enum.GetName(typeof(Status), int.Parse(task.OldValue ?? "0")),
                StartTime = task.StartTime,
                PauseTime = task.PauseTime,
                ProjectName = task.ProjectName,
                Time_Difference = task.Time_Difference,
                Total_Working_Hours = task.Total_Working_Hours
            });

            return taskReportDto;
        }
    }
}
