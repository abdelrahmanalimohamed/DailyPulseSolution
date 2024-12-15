using DailyPulse.Application.Abstraction;
using DailyPulse.Domain.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DailyPulse.Infrastructure.JWT
{
    public class JwtTokenGenerator : ITokenGenerator
    {
        private readonly string _key;
        private readonly string _issuer;

        public JwtTokenGenerator(string _key, string _issuer)
        {
            this._key = _key;
            this._issuer = _issuer;
        }
        public string GenerateToken(Guid userId, EmployeeRole role)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(ClaimTypes.Role, role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}