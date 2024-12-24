using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Login;
using DailyPulse.Application.DTO;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.LoginHandlers
{
	public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponseDTO>
	{
		private readonly IGenericRepository<Employee> _repository;
		private readonly ITokenGenerator _jwtTokenGenerator;
		public LoginQueryHandler(IGenericRepository<Employee> _repository, ITokenGenerator _jwtTokenGenerator)
		{
			this._repository = _repository;
			this._jwtTokenGenerator = _jwtTokenGenerator;
		}
		public async Task<LoginResponseDTO> Handle(LoginQuery request, CancellationToken cancellationToken)
		{
			var employee = await _repository.GetFirstOrDefault(
							x => x.username == request.username,
							cancellationToken);

			if (employee == null || !BCrypt.Net.BCrypt.Verify(request.password, employee.password))
			{
				// Return a response indicating invalid credentials
				return new LoginResponseDTO
				{
					IsSuccess = false,
					Role = null,
					Token = null,
					UserId = null,
				};
			}

			var token = _jwtTokenGenerator.GenerateToken(employee.Id, employee.Role);

			var loginResponse = new LoginResponseDTO
			{
				IsSuccess = true,
				Role = employee.Role.ToString(),
				Token = token,
				UserId = employee.Id,
			};

			return loginResponse;
		}
	}
}
