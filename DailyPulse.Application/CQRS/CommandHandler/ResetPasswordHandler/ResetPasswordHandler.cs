using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.ResetPassword;
using DailyPulse.Domain.Entities;
using MediatR;
using System.Security.Claims;

namespace DailyPulse.Application.CQRS.CommandHandler.ResetPasswordHandler;
internal sealed class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, bool>
{
	private readonly IGenericRepository<Employee> _empRepository;
	private readonly ITokenGenerator _tokenGenerator;
	public ResetPasswordHandler
		(IGenericRepository<Employee> empRepository,
		ITokenGenerator tokenGenerator)
	{
		_empRepository = empRepository;
		_tokenGenerator = tokenGenerator;
	}
	public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
	{
		var validToken = _tokenGenerator.ValidateToken(request.Token);
		if (validToken is null)
		{
			throw new Exception("Token is Exprired");
		}
		var nameIdentifierClaim = validToken.FindFirst(ClaimTypes.NameIdentifier);

		if (nameIdentifierClaim == null || string.IsNullOrEmpty(nameIdentifierClaim.Value))
		{
			throw new Exception("The token does not contain a valid NameIdentifier claim.");
		}

		var empId = Guid.Parse(nameIdentifierClaim.Value);

		var employee = await _empRepository.GetFirstOrDefault(x => x.Id == empId , cancellationToken);

		employee.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
		await _empRepository.UpdateAsync(employee, cancellationToken);

		return true;
	}
}
