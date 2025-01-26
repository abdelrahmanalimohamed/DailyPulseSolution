namespace DailyPulse.Application.Abstraction
{
	public interface IEmailServices
	{
		Task SendEmailAsync(string toEmail, string subject, string body);
	}
}