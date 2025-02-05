using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace DailyPluse.WebAPI.Middleware
{
	public class ExceptionHandling
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandling> _logger;

		public ExceptionHandling(RequestDelegate next, ILogger<ExceptionHandling> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			_logger.LogError(exception, "An exception occurred.");

			context.Response.ContentType = "application/json";
			var statusCode = StatusCodes.Status500InternalServerError;
			var result = new { error = "An unexpected error occurred." };

			// Check for custom exceptions and handle them specifically
			if (exception is DuplicateNameException duplicateNameException)
			{
				statusCode = StatusCodes.Status400BadRequest; // Bad Request for business rule violations
				result = new { error = duplicateNameException.Message };
			}

			context.Response.StatusCode = statusCode;
			return context.Response.WriteAsJsonAsync(result);
		}

		private bool IsUniqueConstraintViolation(DbUpdateException exception)
		{
			// Check the inner exception message or specific database error codes
			if (exception.InnerException is not null)
			{
				var message = exception.InnerException.Message;

				// For SQL Server
				if (message.Contains("Cannot insert duplicate key"))
				{
					return true;
				}

				// For MySQL/MariaDB
				if (message.Contains("Duplicate entry"))
				{
					return true;
				}
			}

			return false;
		}
	}
}
