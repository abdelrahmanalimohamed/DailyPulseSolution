using AutoMapper;
using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Employees;
using DailyPulse.Application.DTO;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.EmployeeHandlers;
internal sealed class GetSelectedEmployeeHandler : IRequestHandler<GetSelectedEmployeeQuery, SelectedEmployeeDTO>
{
	private readonly IGenericRepository<Employee> _empRepository;
	private readonly IMapper _mapper;
	public GetSelectedEmployeeHandler(
		IGenericRepository<Employee> empRepository,
		IMapper mapper)
	{
		_empRepository = empRepository;
		_mapper = mapper;
	}

	public async Task<SelectedEmployeeDTO> Handle(GetSelectedEmployeeQuery request, CancellationToken cancellationToken)
	{
		var employee = await _empRepository.GetFirstOrDefault
			(predicate: x => x.Id == request.EmployeeId,
			cancellationToken);

		var selectedEmployeeDTO = _mapper.Map<SelectedEmployeeDTO>(employee);
		return selectedEmployeeDTO;
	}
}
