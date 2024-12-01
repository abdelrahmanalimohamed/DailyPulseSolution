using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Tasks;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.CQRS.CommandHandler.TasksHandlers
{
    public class UpdateTaskStatusByEmployeeHandler : IRequestHandler<UpdateTaskStatusByEmployeeCommand>
    {
        private readonly IGenericRepository<DailyPulse.Domain.Entities.Task> _repository;

        private readonly IGenericRepository<RejectedTasks> _rejectedtasksRepo;

        private readonly IGenericRepository<TaskStatusLogs> _taskstatusLogsrepo;
        public UpdateTaskStatusByEmployeeHandler(
            IGenericRepository<DailyPulse.Domain.Entities.Task> _repository ,
            IGenericRepository<RejectedTasks> _rejectedtasksRepo,
            IGenericRepository<TaskStatusLogs> _taskstatusLogsrepo)
        {
            this._repository = _repository;
            this._rejectedtasksRepo = _rejectedtasksRepo;
            this._taskstatusLogsrepo = _taskstatusLogsrepo;
        }
        public async Task Handle(UpdateTaskStatusByEmployeeCommand request, CancellationToken cancellationToken)
        {
            var task = await _repository.GetByIdAsync(request.TaskId)
               ?? throw new KeyNotFoundException("Task not found");

            var oldStatus = task.Status;

            task.Status = request.Action == "Accepted" ? Status.InProgress : Status.RequestToReAssign;

            task.IsRejectedByEmployee = task.Status == Status.RequestToReAssign;

            await _repository.UpdateAsync(task, cancellationToken);

            await SaveTaskStatusLog(task.Id, oldStatus, task.Status, cancellationToken);

           // await RejectionReasons(task.Id , task.EmpId , request)
        }

        private async Task SaveTaskStatusLog(
                Guid taskId,
                Status OldStatus,
                Status NewStatus, CancellationToken cancellationToken)
        {
            var taskStatusLogs = new TaskStatusLogs
            {
                TaskId = taskId,
                OldStatus = OldStatus,
                NewStatus = NewStatus
            };

            await _taskstatusLogsrepo.AddAsync(taskStatusLogs, cancellationToken);
        }

        private async Task RejectionReasons(Guid TaskId , Guid EmpId , string RejectionReasons , CancellationToken cancellationToken)
        {
            var rejectedTask = new RejectedTasks
            {
                EmpId = EmpId,
                TaskId = TaskId,
                RejectionReasons = RejectionReasons,
            };

            await _rejectedtasksRepo.AddAsync(rejectedTask, cancellationToken);
        }
    }
}