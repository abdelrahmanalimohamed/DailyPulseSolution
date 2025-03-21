using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Reassign;
using DailyPulse.Application.DTO;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.CQRS.CommandHandler.ReassignHandlers
{
    public class CreateReAssignationHandler : IRequestHandler<CreateReAssignationCommand>
    {
        private readonly IGenericRepository<ReAssign> _repository;

        private readonly IGenericRepository<DailyPulse.Domain.Entities.Task> _taskRepository;

        private readonly IGenericRepository<TaskStatusLogs> _taskstatusLogsrepo;

        public CreateReAssignationHandler(
            IGenericRepository<ReAssign> _repository, 
            IGenericRepository<Domain.Entities.Task> taskRepository,
            IGenericRepository<TaskStatusLogs> taskstatusLogsrepo)
        {
            this._repository = _repository;
            _taskRepository = taskRepository;
            _taskstatusLogsrepo = taskstatusLogsrepo;
        }
        public async Task Handle(CreateReAssignationCommand request, CancellationToken cancellationToken)
        {
            var reAssign = new ReAssign
            {
                EmpId = request.EmpId,
                TeamLeadId = request.TeamLeadId,
                TaskId = request.TaskId,
            };

            await _repository.AddAsync(reAssign , cancellationToken);

            await UpdateAssignation(request.TaskId, request.EmpId, request.MachineName , cancellationToken);
        }
        private async Task UpdateAssignation(Guid taskId, Guid empId, string machineName , CancellationToken cancellationToken) 
        {
            var task = await _taskRepository.GetByIdAsync(taskId , cancellationToken);

            var oldStatus = task.Status;
            task.EmpId = empId;
            task.Status = Status.New;

            await _taskRepository.UpdateAsync(task, cancellationToken);

			SaveTaskStatusDTO saveTaskStatusDTO = new SaveTaskStatusDTO(
		        task.Id, oldStatus, task.Status, machineName);

			await SaveTaskStatusLog(saveTaskStatusDTO, cancellationToken);
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
	}
}