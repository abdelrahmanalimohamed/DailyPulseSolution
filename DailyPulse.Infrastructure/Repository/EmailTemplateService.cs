using DailyPulse.Application.Abstraction;

namespace DailyPulse.Infrastructure.Repository
{
	public class EmailTemplateService : IEmailTemplateService
	{
		public string GenerateResetPasswordEmailBody(string resetpasswordLink)
		{
			var template = @"
            <html>
                <body>
                    <p>Reset your password by clicking the link below:</p>
                    <p><a href='{0}'>Reset Password</a></p>
                </body>
            </html>";

			return string.Format(template, resetpasswordLink);
		}

		public string GenerateVerificationEmailBody(string verificationLink)
		{
			var template = @"
            <html>
                <body>
                    <p>Please verify your account by clicking the link below:</p>
                    <p><a href='{0}'>Verify Email</a></p>
                </body>
            </html>";

			return string.Format(template, verificationLink);
		}
		public string GetVerificationEmailSubject()
		{
			return "Verify Your Email";
		}

		public string ResetPasswordEmailSubject()
		{
			return "Reset Password";
		}
	}
}