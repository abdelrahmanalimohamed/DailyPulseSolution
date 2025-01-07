using DailyPluse.WebAPI.Swagger;
using DailyPulse.Application.DependenyInjectionServices;
using DailyPulse.Infrastructure.DependencyInjectionService;
using DailyPulse.Infrastructure.Persistence;
using DailyPulse.Infrastructure.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DailyPluse.WebAPI
{
	public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            builder.Services
                  .AddApplication()
                  .AddInfrastructure(builder.Configuration);

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });

			var app = builder.Build();
			
			using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
				context.Database.Migrate();
				await SeedData.Initialize(context);
            }

            app.UseCors(policy => policy.AllowAnyHeader()
                                        .AllowAnyMethod()
                                        .SetIsOriginAllowed(origin => true)
                                        .AllowCredentials());

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}
