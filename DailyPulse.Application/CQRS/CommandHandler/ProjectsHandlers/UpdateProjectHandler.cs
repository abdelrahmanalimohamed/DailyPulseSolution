using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Projects;
using DailyPulse.Domain.Entities;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.CQRS.CommandHandler.ProjectsHandlers
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand>
    {
        private readonly IGenericRepository<Project> _repository;

        public UpdateProjectHandler(IGenericRepository<Project> _repository)
        {
            this._repository = _repository;
        }
        public async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetByIdAsync(request.projectId);
            if (project == null)
            {
                throw new Exception("Project not found");
            }

            project.Name = request.projectName;
            project.RegionId = request.RegionId;
            project.Description = request.description;
            project.LocationId = request.LocationId;
            project.ProjectNo = request.projectNo;

            await _repository.UpdateAsync(project, cancellationToken);
        }
    }
}
