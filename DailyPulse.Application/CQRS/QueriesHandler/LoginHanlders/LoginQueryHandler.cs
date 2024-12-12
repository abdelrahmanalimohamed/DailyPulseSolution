using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Login;
using DailyPulse.Application.DTO;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.LoginHanlders
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponseDTO>
    {
        private readonly IGenericRepository<Employee> _repository;
        private readonly ITokenGenerator _jwtTokenGenerator;
        public LoginQueryHandler(IGenericRepository<Employee> _repository , ITokenGenerator _jwtTokenGenerator)
        {
            this._repository = _repository;
            this._jwtTokenGenerator = _jwtTokenGenerator;
        }
        public async Task<LoginResponseDTO> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            //string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.password);

            var employee = await _repository.GetFirstOrDefault(
                x => x.username == request.username &&
                x.password == request.password , 
                cancellationToken);

           var token =  _jwtTokenGenerator.GenerateToken(employee.Id, employee.Role);

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
