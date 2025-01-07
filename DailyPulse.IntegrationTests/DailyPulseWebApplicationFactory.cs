using DailyPluse.WebAPI;
using DailyPulse.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DailyPulse.IntegrationTests
{
	public class DailyPulseWebApplicationFactory : WebApplicationFactory<Program>
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureTestServices(services =>
			{
				services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

				var connString = GetConnectionString();
				//services.AddMySql<ApplicationDbContext>(connString, ServerVersion.AutoDetect(connString));
				services.AddDbContext<ApplicationDbContext>(options =>
						options.UseMySql(connString, ServerVersion.AutoDetect(connString)));


				var dbContext = CreateDbContext(services);
				dbContext.Database.EnsureDeleted();
				//dbContext.Database.EnsureCreated();
			});
		}

		private static string? GetConnectionString()
		{
			var configuration = new ConfigurationBuilder()
									.AddUserSecrets<DailyPulseWebApplicationFactory>()
									.Build();

			var connString = configuration.GetConnectionString("DefaultConnection");
			return connString;
		}

		private static ApplicationDbContext CreateDbContext(IServiceCollection serviceDescriptors)
		{
			var serviceProvider = serviceDescriptors.BuildServiceProvider();
			var scope = serviceProvider.CreateScope();
			var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
			return dbContext;
		}
	}
}