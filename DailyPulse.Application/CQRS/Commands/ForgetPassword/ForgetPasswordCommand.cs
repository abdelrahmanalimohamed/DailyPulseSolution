using MediatR;

namespace DailyPulse.Application.CQRS.Commands.ForgetPassword;
public sealed class ForgetPasswordCommand : IRequest<bool>
{
	public string Email { get; set; }
}