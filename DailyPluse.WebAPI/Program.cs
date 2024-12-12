using AutoMapper;
using DailyPulse.Application.Abstraction;
using DailyPulse.Application.DependenyInjectionServices;
using DailyPulse.Application.Mapper;
using DailyPulse.Infrastructure.DependencyInjectionService;
using DailyPulse.Infrastructure.Persistence;
using DailyPulse.Infrastructure.Repository;
using DailyPulse.Infrastructure.Seeding;
using Microsoft.EntityFrameworkCore;

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

            builder.Services
                  .AddApplication()
                  .AddInfrastructure();

            var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseMySql(defaultConnectionString, 
                ServerVersion.AutoDetect(defaultConnectionString)));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
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

            app.UseAuthorization();


            app.MapControllers();

            await app.RunAsync();
        }
    }
}
