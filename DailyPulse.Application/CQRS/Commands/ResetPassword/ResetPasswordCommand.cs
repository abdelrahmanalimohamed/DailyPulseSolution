using MediatR;

namespace DailyPulse.Application.CQRS.Commands.ResetPassword;
public sealed class ResetPasswordCommand : IRequest<bool>
{
	public string Token { get; set; }
	public string Password { get; set; }
}