using AutoMapper;
using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Tasks;
using DailyPulse.Application.DTO;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.TaskHandlers
{
	class GetTaskTypesDetailsHandler : IRequestHandler<GetTaskTypeDetailsQuery, 
										IEnumerable<TaskTypesDetailsDTO>>
	{
		private readonly IGenericRepository<TaskTypeDetails> _repository;
		private readonly IMapper _mapper;
		public GetTaskTypesDetailsHandler(
			IGenericRepository<TaskTypeDetails> repository, 
			IMapper mapper)
		{
			_repository=repository;
			_mapper=mapper;
		}
		public async Task<IEnumerable<TaskTypesDetailsDTO>> Handle(
			GetTaskTypeDetailsQuery request, 
			CancellationToken cancellationToken)
		{
			var results = await _repository.FindAsync(x => x.TaskTypeId == request.tasktypeId , cancellationToken);

			return _mapper.Map<IEnumerable<TaskTypesDetailsDTO>>(results);
		}
	}
}
