namespace DailyPulse.Application.Abstraction
{
	public interface IEmailTemplateService
	{
		string GenerateVerificationEmailBody(string verificationLink);
		string GetVerificationEmailSubject();
		string GenerateResetPasswordEmailBody(string resetpasswordLink);
		string ResetPasswordEmailSubject();
	}
}