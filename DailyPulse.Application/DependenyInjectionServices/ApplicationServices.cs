using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DailyPulse.Application.DependenyInjectionServices
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(ApplicationServices).Assembly;

            services.AddMediatR(configuration =>
              configuration.RegisterServicesFromAssembly(assembly));

            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}