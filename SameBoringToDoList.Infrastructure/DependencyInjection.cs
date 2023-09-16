using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SameBoringToDoList.Application.Authentication;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Infrastructure.Authentication;
using SameBoringToDoList.Infrastructure.Authentication.Options;
using SameBoringToDoList.Infrastructure.Persistence;
using SameBoringToDoList.Infrastructure.Persistence.Options;
using SameBoringToDoList.Infrastructure.Persistence.Repositories;
using SameBoringToDoList.Shared;

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
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            return services;
        }
    }
}
