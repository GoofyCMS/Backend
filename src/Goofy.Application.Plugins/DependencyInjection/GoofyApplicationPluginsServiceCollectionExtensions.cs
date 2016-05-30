using Goofy.Application.Core.DependencyInjection;
using Goofy.Application.Plugins.Abstractions;
using Goofy.Application.Plugins.Services;
using Goofy.Domain.Plugins.Service.Data;
using Goofy.Infrastructure.Plugins.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Goofy.Application.Plugins.DependencyInjection
{
    public static class GoofyApplicationPluginsServiceCollectionExtensions
    {
        public static IServiceCollection AddPluggableCore(this IServiceCollection services)
        {
            services.AddGoofyCore();
            services.AddSingleton<IGoofyAssemblyProvider, GoofyAssemblyProvider>();
            services.AddSingleton<IPluginAssemblyProvider, GoofyPluginAssemblyProvider>();
            services.AddSingleton(typeof(PluginServiceMapper<,>));
            services.AddSingleton(typeof(IPluginUnitOfWork), typeof(PluginsContext));
            services.AddSingleton<IEngine, PluginBasedEngine>();
            return services;
        }
    }
}
