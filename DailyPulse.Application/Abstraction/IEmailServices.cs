namespace DailyPulse.Application.Abstraction
{
	public interface IEmailServices
	{
		void SendEmailAsync(string toEmail, string subject, string body);
	}
}