using Microsoft.Extensions.DependencyInjection;
using SameBoringToDoList.Shared.Services;

namespace SameBoringToDoList.Shared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddShared(this IServiceCollection services)
        {
            services.AddHostedService<AppInitializer>();

            return services;
        }
    }
}
