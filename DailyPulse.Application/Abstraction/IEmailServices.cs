﻿namespace DailyPulse.Application.Abstraction
{
	public interface IEmailServices
	{
		Task<bool> SendEmailAsync(string toEmail, string subject, string body);
	}
}