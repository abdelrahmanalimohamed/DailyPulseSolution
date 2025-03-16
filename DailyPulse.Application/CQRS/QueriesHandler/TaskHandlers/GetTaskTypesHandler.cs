using AutoMapper;
using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Tasks;
using DailyPulse.Application.DTO;
using DailyPulse.Application.ViewModel;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.TaskHandlers
{
	public class GetTaskTypesHandler : IRequestHandler<GetTaskTypesQuery, IEnumerable<TaskTypeDTO>>
    {
		private readonly IGenericRepository<TaskType> _repository;
		private readonly IMapper _mapper;
		public GetTaskTypesHandler(
			IGenericRepository<TaskType> repository,
			IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}
		public async Task<IEnumerable<TaskTypeDTO>> Handle(GetTaskTypesQuery request, CancellationToken cancellationToken)
		{
			var results = await _repository.GetAllAsync();

			return _mapper.Map<IEnumerable<TaskTypeDTO>>(results);
		}
	}
}
