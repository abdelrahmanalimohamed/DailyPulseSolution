using DailyPulse.Domain.Enums;

namespace DailyPulse.Application.Abstraction
{
    public interface ITokenGenerator
    {
        string GenerateToken(Guid userId, EmployeeRole role);
    }
}