using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Reassign;
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

            await UpdateAssignation(request.TaskId, request.EmpId, cancellationToken);
        }

        private async Task UpdateAssignation(Guid TaskId, Guid EmpId, CancellationToken cancellationToken) 
        {
            var task = await _taskRepository.GetByIdAsync(TaskId , cancellationToken);

            var oldStatus = task.Status;
            task.EmpId = EmpId;
            task.Status = Status.New;
            await _taskRepository.UpdateAsync(task, cancellationToken);
            await SaveTaskStatusLog(task.Id , oldStatus , task.Status , cancellationToken);
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

    }
}