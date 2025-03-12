using System.Data;
using System.Text.RegularExpressions;
using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Projects;
using DailyPulse.Application.Extensions;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.CQRS.CommandHandler.ProjectsHandlers
{
    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand>
    {
        private readonly IGenericRepository<Project> _repository;

       // private readonly IGenericRepository<ProjectsScopes> _projectScopeAssignmentsRepository;

        public CreateProjectHandler(IGenericRepository<Project> _repository )
        {
            this._repository = _repository;
           // this._projectScopeAssignmentsRepository = _projectScopeAssignmentsRepository;
        }

        public async Task Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
			var normalizedName = request.Name.RemoveWhitespace();

			var existingProject = await _repository.GetFirstOrDefault(
						prj => prj.Name.Trim().ToLower() == normalizedName.ToLower(),
						cancellationToken);

			if (existingProject != null)
			{
				throw new DuplicateNameException("A Project with the same name already exists.");
			}

			string trade = Regex.Replace(request.TradeId, @"\s+", "");

            var project = new Project
            {
                Description = request.Description,
                LocationId = request.LocationId,
                Trade = Enum.TryParse(trade, true, out Treats role)
                     ? role : throw new ArgumentException($"Invalid trade: {request.TradeId}"),
                Name = request.Name,
                RegionId = request.RegionId ,
                EmployeeId = request.EmployeeId
            };

             await _repository.AddAsync(project, cancellationToken);

          //  await InsertProjectScopes(insertedProject, request.ScopeOfWorksSelections , cancellationToken);

        }
    }
}