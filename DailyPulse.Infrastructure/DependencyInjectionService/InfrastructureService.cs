using DailyPulse.Application.Abstraction;
using DailyPulse.Application.Mapper;
using DailyPulse.Domain.Enums;
using DailyPulse.Infrastructure.JWT;
using DailyPulse.Infrastructure.Persistence;
using DailyPulse.Infrastructure.Repository;
using DailyPulse.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace DailyPulse.Infrastructure.DependencyInjectionService
{
    public static class InfrastructureService
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services , IConfiguration Configuration)
        {
            var defaultConnectionString = Configuration.GetConnectionString("DefaultConnection");
			var host = Configuration.GetValue<string>("Hosts:host");

			services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

			services.AddDbContext<ApplicationDbContext>(
                options => options.UseMySql(defaultConnectionString,
                ServerVersion.AutoDetect(defaultConnectionString)));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddSingleton<ITokenGenerator>(new JwtTokenGenerator(
                "e565deac6cb20c6d458d8efcac8a9db5091669a8ce8c6423bf51d58c6a9660eca9ef0fc987100365a9f21e2347ec2a4d3e7310c2d04c4a8162f5ef34c9560ac45f06125f409e3ef968a7c716b9f03ff0335311d7a98614ab4a3c7499bc74f6a815be07d96d89103003f6b8b1e1499a6645a44ba540cdd802b3742dba378fb9c34b0c8075dc4735f293b9711f79e31d582142964a94af6a2501301156a8088378df9ea580a6a8b6778bd40864ccb99a304e349be3158bfa3783bdc1680e9a1b0536660dbe3af8aeccc9d7eaf1c0f2e5d0021381fbc33e254127cd9d668b4fb03c797342c465cb7fc7b91e9a3a090247c91ef0d329f83cda73671b0bfd8d928713",
				host));

			services.AddTransient<IEmailServices, EmailServices>();
			services.AddTransient<IEmailTemplateService, EmailTemplateService>();

			services.AddAutoMapper(typeof(MappingProfile));

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			  .AddJwtBearer(options =>
              {
                  options.SaveToken = true;
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = host,
                      ValidAudience = host,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("e565deac6cb20c6d458d8efcac8a9db5091669a8ce8c6423bf51d58c6a9660eca9ef0fc987100365a9f21e2347ec2a4d3e7310c2d04c4a8162f5ef34c9560ac45f06125f409e3ef968a7c716b9f03ff0335311d7a98614ab4a3c7499bc74f6a815be07d96d89103003f6b8b1e1499a6645a44ba540cdd802b3742dba378fb9c34b0c8075dc4735f293b9711f79e31d582142964a94af6a2501301156a8088378df9ea580a6a8b6778bd40864ccb99a304e349be3158bfa3783bdc1680e9a1b0536660dbe3af8aeccc9d7eaf1c0f2e5d0021381fbc33e254127cd9d668b4fb03c797342c465cb7fc7b91e9a3a090247c91ef0d329f83cda73671b0bfd8d928713")) // Replace with a strong key
                  };
			  });

			services.AddAuthorization(options =>
			{
				options.AddPolicy("AdminRoles", policy =>
	            	policy.RequireClaim(ClaimTypes.Role, EmployeeRole.Admin.ToString()));

				options.AddPolicy("SeniorRoles", policy =>
					policy.RequireClaim(ClaimTypes.Role, EmployeeRole.Senior.ToString()));
			});

			return services;
        }
    }
}