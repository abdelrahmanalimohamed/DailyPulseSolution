using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Projects;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.ProjectHandlers
{
    public class GetProjectsHandler : IRequestHandler<GetProjectsQuery, IEnumerable<Project>>
    {
        private readonly IGenericRepository<Project> _repository;

        public GetProjectsHandler(IGenericRepository<Project> _repositor)
        {
           this._repository = _repositor;
        }
        public async Task<IEnumerable<Project>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
    }
}