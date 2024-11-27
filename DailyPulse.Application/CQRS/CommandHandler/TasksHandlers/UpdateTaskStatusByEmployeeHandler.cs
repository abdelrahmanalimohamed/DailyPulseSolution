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

        public UpdateTaskStatusByEmployeeHandler(
            IGenericRepository<DailyPulse.Domain.Entities.Task> _repository ,
            IGenericRepository<RejectedTasks> _rejectedtasksRepo)
        {
            this._repository = _repository;
            this._rejectedtasksRepo = _rejectedtasksRepo;
        }
        public async Task Handle(UpdateTaskStatusByEmployeeCommand request, CancellationToken cancellationToken)
        {
            var task = await _repository.GetByIdAsync(request.TaskId)
               ?? throw new KeyNotFoundException("Task not found");

            task.Status = request.Action == "Accepted" ? Status.InProgress : Status.RequestToReAssign;

            task.IsRejectedByEmployee = task.Status == Status.RequestToReAssign;

            await _repository.UpdateAsync(task, cancellationToken);

           // await RejectionReasons(task.Id , task.EmpId , request)
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