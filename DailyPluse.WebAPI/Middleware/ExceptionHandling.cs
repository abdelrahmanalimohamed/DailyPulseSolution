using System.Data;

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

			if (exception is Exception ex)
			{
				statusCode = StatusCodes.Status400BadRequest;
				result = new { error = ex.Message };
			}

			if (exception is DuplicateNameException duplicateNameException)
			{
				statusCode = StatusCodes.Status400BadRequest;
				result = new { error = duplicateNameException.Message };
			}

			context.Response.StatusCode = statusCode;
			return context.Response.WriteAsJsonAsync(result);
		}
	}
}
