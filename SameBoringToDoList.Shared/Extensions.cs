using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SameBoringToDoList.Shared.Services;

namespace SameBoringToDoList.Shared
{
    public static class Extensions
    {
        public static TOptions GetOptions<TOptions>(this IConfiguration configuration, string sectionName) where TOptions : new()
        {
            var options = new TOptions();
            configuration.GetSection(sectionName).Bind(options);

            return options;
        }
    }
}
