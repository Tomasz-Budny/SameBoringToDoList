using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SameBoringToDoList.Application.Authentication;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Infrastructure.Authentication;
using SameBoringToDoList.Infrastructure.Authentication.Options;
using SameBoringToDoList.Infrastructure.Persistence;
using SameBoringToDoList.Infrastructure.Persistence.Options;
using SameBoringToDoList.Infrastructure.Persistence.Repositories;
using SameBoringToDoList.Shared;
using System.Text;

namespace SameBoringToDoList.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuth(configuration);
            services.AddScoped<IToDoListRepository, ToDoListRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            var options = configuration.GetOptions<SqlOptions>("SqlServer");
            services.AddDbContext<SameBoringToDoListDbContext>(ctx => 
                ctx.UseSqlServer(options.ConnectionString));
            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetOptions<JwtSettings>(JwtSettings.SectionName);
            services.AddSingleton(Options.Create(jwtSettings));

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret))
                });

            return services;
        }
    }
}
