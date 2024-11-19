using System.Linq.Expressions;
using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Projects;
using DailyPulse.Application.DTO;
using DailyPulse.Application.ViewModel;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.ProjectHandlers
{
    public class GetRelatedProjectScopesHandler : IRequestHandler<GetRelatedProjectScopesQuery, IEnumerable<ScopeOfWorkViewModel>>
    {
        private readonly IGenericRepository<ProjectsScopes> _repository;

        public GetRelatedProjectScopesHandler(IGenericRepository<ProjectsScopes> _repository)
        {
            this._repository = _repository;
        }
        public async Task<IEnumerable<ScopeOfWorkViewModel>> Handle(GetRelatedProjectScopesQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<ProjectsScopes, object>>>
                 {
                     project => project.Project ,
                     project => project.ScopeOfWork
                 };

            var projectScopes = await _repository.FindWithIncludeAsync(x => x.ProjectId == request.ProjectId, 
                includes, cancellationToken);

            var scopeOfWorksViewModel = projectScopes.Select(prjscopes => new ScopeOfWorkViewModel
            {
                Id = prjscopes.ScopeOfWork.Id,
                Name = prjscopes.ScopeOfWork.Name
            });

            return scopeOfWorksViewModel;

        }
    }
}
