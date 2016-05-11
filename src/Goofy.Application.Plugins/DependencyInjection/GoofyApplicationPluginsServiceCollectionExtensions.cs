using Goofy.Application.Core.DependencyInjection;
using Goofy.Domain.Core.Abstractions;
using Goofy.Domain.Plugins;
using Microsoft.Extensions.DependencyInjection;

namespace Goofy.Application.Plugins.DependencyInjection
{
    public static class GoofyApplicationPluginsServiceCollectionExtensions
    {
        public static IServiceCollection AddPluginBasedCore(this IServiceCollection services)
        {
            services.AddGoofyCore();
            services.AddSingleton<IPluginAssemblyProvider, GoofyPluginAssemblyProvider>();
            services.AddSingleton<IEngine, PluginBasedEngine>();
            return services;
        }
    }
}
