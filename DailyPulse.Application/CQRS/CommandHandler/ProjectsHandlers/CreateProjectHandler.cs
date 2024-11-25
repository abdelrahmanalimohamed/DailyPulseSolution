using System.Text.RegularExpressions;
using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Projects;
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
            string trade = Regex.Replace(request.TradeId, @"\s+", "");

            var project = new Project
            {
                Description = request.Description,
                LocationId = request.LocationId,
                Trade = Enum.TryParse(trade, true, out Trades role)
                     ? role : throw new ArgumentException($"Invalid trade: {request.TradeId}"),
                Name = request.Name,
                RegionId = request.RegionId
            };

            var insertedProject = await _repository.AddAsyncWithReturnEntity(project, cancellationToken);

          //  await InsertProjectScopes(insertedProject, request.ScopeOfWorksSelections , cancellationToken);

        }
    }
}