using AutoMapper;
using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.TaskNewRequirements;
using DailyPulse.Application.ViewModel;
using DailyPulse.Domain.Entities;
using MediatR;
using System.Threading.Tasks;

namespace DailyPulse.Application.CQRS.QueriesHandler.TaskNewRequirementsHandlers;
internal sealed class GetTaskNewRequirementsHandler 
	: IRequestHandler<GetTaskNewRequirementsQuery, 
	IEnumerable<TaskNewRequirementsViewModel>>
{
	private readonly IGenericRepository<TaskNewRequirements> _taskrequirementRepository;
	private readonly IMapper _mapper;
	public GetTaskNewRequirementsHandler(
		IGenericRepository<TaskNewRequirements> taskrequirementRepository , 
		IMapper mapper)
	{
		_taskrequirementRepository = taskrequirementRepository;
		_mapper = mapper;
	}
	public async Task<IEnumerable<TaskNewRequirementsViewModel>> Handle(GetTaskNewRequirementsQuery request, CancellationToken cancellationToken)
	{
		var results = await _taskrequirementRepository.FindAsync
			(x => x.TaskId == request.TaskId, cancellationToken);

		var tasknewRequirementViewModel = _mapper.Map<List<TaskNewRequirementsViewModel>>(results);
		return tasknewRequirementViewModel;
	}
}
