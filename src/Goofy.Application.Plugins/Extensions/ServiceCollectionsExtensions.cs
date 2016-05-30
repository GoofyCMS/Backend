using Goofy.Application.Plugins.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Goofy.Application.Plugins.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static void StartEngine(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var engine = serviceProvider.GetRequiredService<IEngine>();
            engine.Start();
        }
    }
}
