using DailyPulse.Application.DTO;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Login
{
    public class LoginQuery : IRequest<LoginResponseDTO>
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}