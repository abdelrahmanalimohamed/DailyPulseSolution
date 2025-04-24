using AutoMapper;
using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Employees;
using DailyPulse.Application.ViewModel;
using DailyPulse.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace DailyPulse.Application.CQRS.QueriesHandler.EmployeeHandlers;

internal sealed class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<AllEmployeesViewModel>>
{
	private readonly IGenericRepository<Employee> _empRepository;
	private readonly IMapper _mapper;
	public GetAllEmployeesHandler(
		IGenericRepository<Employee> empRepository ,
		IMapper mapper)
	{
		_empRepository = empRepository;
		_mapper = mapper;
	}
	public async Task<IEnumerable<AllEmployeesViewModel>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
	{
		var includes = new List<Expression<Func<Employee, object>>>
				 {
					 task => task.ReportTo
                 };

		var employess = await _empRepository.FindWithIncludeAsync(null,includes , cancellationToken);
		var employessViewModel = _mapper.Map<List<AllEmployeesViewModel>>(employess);
		return employessViewModel;

	}
}
