using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Tasks;
using DailyPulse.Application.DTO;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.CQRS.CommandHandler.TasksHandlers
{
    public class UpdateTaskStatusByEmployeeHandler : IRequestHandler<UpdateTaskStatusByEmployeeCommand>
    {
        private readonly IGenericRepository<DailyPulse.Domain.Entities.Task> _repository;

        private readonly IGenericRepository<EmployeeRejectedTasks> _rejectedtasksRepo;

        private readonly IGenericRepository<TaskStatusLogs> _taskstatusLogsrepo;
        public UpdateTaskStatusByEmployeeHandler(
            IGenericRepository<DailyPulse.Domain.Entities.Task> _repository ,
            IGenericRepository<EmployeeRejectedTasks> _rejectedtasksRepo,
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

            task.Status = request.Action == "Accepted" ? TasksStatus.InProgress : TasksStatus.RequestToReAssign;

            task.IsRejectedByEmployee = task.Status == TasksStatus.RequestToReAssign;

            await _repository.UpdateAsync(task, cancellationToken);

			SaveTaskStatusDTO saveTaskStatusDTO = new SaveTaskStatusDTO(
	            task.Id, oldStatus, task.Status, request.MachineName);

			await SaveTaskStatusLog(saveTaskStatusDTO, cancellationToken);

           // await RejectionReasons(task.Id , task.EmpId , request)
        }
		private async Task SaveTaskStatusLog(
             SaveTaskStatusDTO saveTaskStatusDTO,
             CancellationToken cancellationToken)
		{
			var taskStatusLogs = new TaskStatusLogs
			{
				TaskId = saveTaskStatusDTO.taskId,
				OldStatus = saveTaskStatusDTO.oldStatus,
				NewStatus = saveTaskStatusDTO.newStatus,
				MachineName = saveTaskStatusDTO.machineName
			};

			await _taskstatusLogsrepo.AddAsync(taskStatusLogs, cancellationToken);
		}

		private async Task RejectionReasons(Guid TaskId , Guid EmpId , string RejectionReasons , CancellationToken cancellationToken)
        {
            var rejectedTask = new EmployeeRejectedTasks
            {
                EmpId = EmpId,
                TaskId = TaskId,
                RejectionReasons = RejectionReasons,
            };

            await _rejectedtasksRepo.AddAsync(rejectedTask, cancellationToken);
        }
    }
}