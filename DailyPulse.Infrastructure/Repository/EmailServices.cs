using DailyPulse.Application.Abstraction;
using MimeKit;
using MailKit.Net.Smtp;
using DailyPulse.Infrastructure.Configurations;
using Microsoft.Extensions.Options;

namespace DailyPulse.Infrastructure.Repository
{
	public class EmailServices : IEmailServices
	{
		private readonly EmailSettings _emailSettings;
		public EmailServices(IOptions<EmailSettings> _emailSettings)
		{
			this._emailSettings = _emailSettings.Value;
		}
		public async Task SendEmailAsync(string toEmail, string subject, string body)
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress(_emailSettings.DisplayName, _emailSettings.Mail));
			message.To.Add(new MailboxAddress("", toEmail));
			message.Subject = subject;

			var bodyBuilder = new BodyBuilder
			{
				HtmlBody = body
			};
			message.Body = bodyBuilder.ToMessageBody();

			using (var client = new SmtpClient())
			{
				await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, useSsl: true);
				await client.AuthenticateAsync(_emailSettings.Mail, _emailSettings.Password);
				await client.SendAsync(message);
				await client.DisconnectAsync(true);
			}
		}
	}
}