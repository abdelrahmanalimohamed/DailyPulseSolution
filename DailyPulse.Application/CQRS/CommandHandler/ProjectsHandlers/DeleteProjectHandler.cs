using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Projects;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.CommandHandler.ProjectsHandlers
{
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, Unit>
    {
        private readonly IGenericRepository<Project> _repository;

        public DeleteProjectHandler(IGenericRepository<Project> _repository)
        {
            this._repository = _repository;
        }
        public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetByIdAsync(request.ProjectId);
            if (project == null)
            {
                throw new KeyNotFoundException("Project not found");
            }

            await _repository.DeleteAsync(project , cancellationToken);
            return Unit.Value;
        }
    }
}
