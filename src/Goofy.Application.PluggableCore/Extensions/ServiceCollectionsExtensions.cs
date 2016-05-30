using Goofy.Application.PluggableCore.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Goofy.Application.PluggableCore.Extensions
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
