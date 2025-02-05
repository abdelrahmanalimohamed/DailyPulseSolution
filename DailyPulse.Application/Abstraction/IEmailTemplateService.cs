namespace DailyPulse.Application.Abstraction
{
	public interface IEmailTemplateService
	{
		string GenerateVerificationEmailBodyAsync(string verificationLink);
		string GetVerificationEmailSubject();
	}
}