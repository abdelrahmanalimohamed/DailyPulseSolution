using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.ForgetPassword;
using DailyPulse.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace DailyPulse.Application.CQRS.CommandHandler.ForgetPasswordHandler;
internal sealed class ForgetPasswordHandler : IRequestHandler<ForgetPasswordCommand, bool>
{
	private readonly IGenericRepository<Employee> _empRepository;
	private readonly ITokenGenerator _jwtTokenGenerator;
	private readonly IEmailServices _emailService;
	private readonly IEmailTemplateService _emailTemplateService;
	private readonly IConfiguration _configuration;
	public ForgetPasswordHandler(
		IGenericRepository<Employee> empRepository,
		ITokenGenerator jwtTokenGenerator,
		IEmailServices emailService,
		IEmailTemplateService emailTemplateService ,
		IConfiguration configuration)
	{
		_empRepository = empRepository;
		_jwtTokenGenerator = jwtTokenGenerator;
		_emailService = emailService;
		_emailTemplateService = emailTemplateService;
		_configuration = configuration;
	}
	public async Task<bool> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
	{
		var employee = await _empRepository.GetFirstOrDefault(x => x.Email == request.Email
													&& x.IsEmailVerified == true, cancellationToken);
		if (employee == null)
		{
			throw new Exception("The user is not found");
		}

		var token = _jwtTokenGenerator.GenerateToken(employee.Id, employee.Role);

		var resetLink = $"{_configuration["Hosts:host"]}/reset-password?token={token}";

		var emailSubject = _emailTemplateService.ResetPasswordEmailSubject();

		var emailBody = _emailTemplateService.GenerateResetPasswordEmailBody(resetLink);

		var isEmailSent = await _emailService.SendEmailAsync(employee.Email, emailSubject, emailBody);

		return isEmailSent;
	}
}
