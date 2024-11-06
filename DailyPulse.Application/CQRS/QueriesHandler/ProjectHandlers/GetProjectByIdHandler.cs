using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Projects;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.ProjectHandlers
{
    public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery , Project>
    {
        private readonly IGenericRepository<Project> _repository;

        public GetProjectByIdHandler(IGenericRepository<Project> _repository)
        {
            this._repository = _repository;
        }
        public async Task<Project> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.ProjectId , cancellationToken);
        }
    }
}
