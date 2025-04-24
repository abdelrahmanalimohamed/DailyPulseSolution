using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Tasks;
using DailyPulse.Application.Extensions;
using DailyPulse.Domain.Enums;
using MediatR;
using System.Data;
using System.Text.RegularExpressions;
using Task = System.Threading.Tasks.Task;


namespace DailyPulse.Application.CQRS.CommandHandler.TasksHandlers
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand>
    {
        private readonly IGenericRepository<DailyPulse.Domain.Entities.Task> _repository;

        public CreateTaskHandler(IGenericRepository<DailyPulse.Domain.Entities.Task> _repository)
        {
            this._repository = _repository;
        }
        public async Task Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
			var normalizedName = request.TaskName.RemoveWhitespace();

			var existingTask = await _repository.GetFirstOrDefault(
						tsk => tsk.Name.Trim().ToLower() == normalizedName.ToLower(),
						cancellationToken);

			if (existingTask != null)
			{
				throw new DuplicateNameException("A Task with the same name already exists.");
			}

            var task = new DailyPulse.Domain.Entities.Task
            {
                Name = request.TaskName,
                //Area = request.Area,
                DateFrom = request.FromDate,
                DateTo = request.ToDate,
                DrawingId = request.DrawingNo,
                EstimatedWorkingHours = request.EstimatedHours,
                EmpId = request.EmployeeId,
                FilePath = request.file,
                ProjectId = request.ProjectId,
                CreatedByMachine = request.MachineName,
                Status = Status.New,
                //ScopeId = request.ScopeId,
                Priority = Enum.TryParse(request.Priority, true, out Priority role)
                     ? role : throw new ArgumentException($"Invalid priority: {request.Priority}"),
                DrawingTitle = request.DrawingTitle,
                Levels = Enum.TryParse(Regex.Replace(request.level, @"[^a-zA-Z0-9]", ""), true, out Levels level)
                ? level
                : throw new ArgumentException($"Invalid level: {request.level}"),

                TaskTypeDetailsId = request.tasktypedetailsId,
                OtherTypes = request.others,
                TaskCreatedBy = request.CreatedBy , 
                TaskDescription = request.TaskDescription
            };

            await _repository.AddAsync(task, cancellationToken);
        }
    }
}