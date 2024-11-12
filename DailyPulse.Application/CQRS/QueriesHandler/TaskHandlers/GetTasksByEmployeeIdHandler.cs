using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Tasks;
using DailyPulse.Application.ViewModel;
using MediatR;
using Task = DailyPulse.Domain.Entities.Task;

namespace DailyPulse.Application.CQRS.QueriesHandler.TaskHandlers
{
    public class GetTasksByEmployeeIdHandler : IRequestHandler<GetTasksByEmployeeIdQuery, IEnumerable<TaskHeaderViewModel>>
    {
        private readonly IGenericRepository<Task> _repository;

        public GetTasksByEmployeeIdHandler(IGenericRepository<Task> _repository)
        {
            this._repository = _repository;
        }

        public async Task<IEnumerable<TaskHeaderViewModel>> Handle(GetTasksByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _repository.FindAsync(x => x.EmpId == request.EmployeeId, cancellationToken);

            var taskHeaderViewModel = tasks.Select(task => new TaskHeaderViewModel
            {
                Id = task.Id,
                Name = task.Name,
            });

            return taskHeaderViewModel;
        }
    }
}
