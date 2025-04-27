using DailyPulse.Domain.Enums;
using System.Security.Claims;

namespace DailyPulse.Application.Abstraction
{
    public interface ITokenGenerator
    {
        string GenerateToken(Guid userId, EmployeeRole role);
        ClaimsPrincipal ValidateToken(string token);
    }
}