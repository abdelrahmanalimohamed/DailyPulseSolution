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

        public CreateReAssignationHandler(
            IGenericRepository<ReAssign> _repository, 
            IGenericRepository<Domain.Entities.Task> taskRepository)
        {
            this._repository = _repository;
            _taskRepository = taskRepository;
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
            task.EmpId = EmpId;
            task.Status = Status.New;
            await _taskRepository.UpdateAsync(task, cancellationToken);
        }

    }
}
