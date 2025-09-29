using AutoMapper;
using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.ProfitCenter;
using DailyPulse.Application.DTO;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.ProfitCenterHandlers;
internal sealed class GetProfitCentersHandler : IRequestHandler<GetProfitCentersQuery, IEnumerable<ProfitCenterDTO>>
{
	private readonly IGenericRepository<ProfitCenter> _repository;
	private readonly IMapper _mapper;
	public GetProfitCentersHandler(
		IGenericRepository<ProfitCenter> repository , 
		IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}
	public async Task<IEnumerable<ProfitCenterDTO>> Handle(GetProfitCentersQuery request, CancellationToken cancellationToken)
	{
		var profitCenters = await _repository.GetAllAsync(cancellationToken);

		var profitCenterDTO = _mapper.Map<IEnumerable<ProfitCenterDTO>>(profitCenters);

		//var profitCenterDTO = profitCenters.Select(project => new ProfitCenterDTO
		//{
		//	Id = project.Id,
		//	Name = project.ProfitCenterDescription ,
		//	ProfitCenterNumber = project.ProfitCenterNumber,
		//});

		return profitCenterDTO;
	}
}
