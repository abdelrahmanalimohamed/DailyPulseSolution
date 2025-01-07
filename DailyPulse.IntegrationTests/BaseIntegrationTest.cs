using DailyPulse.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DailyPulse.IntegrationTests
{
	public abstract class BaseIntegrationTest : IClassFixture<DailyPulseWebApplicationFactory>
	{
		private readonly IServiceScope _scope;
		protected readonly ISender sender;
		protected readonly ApplicationDbContext applicationDbContext;
		protected readonly HttpClient _httpClient;

		protected BaseIntegrationTest(DailyPulseWebApplicationFactory factory)
		{
			_scope = factory.Services.CreateScope();
			sender = _scope.ServiceProvider.GetRequiredService<ISender>();
			applicationDbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
			_httpClient = factory.CreateClient();
		}
	}
}