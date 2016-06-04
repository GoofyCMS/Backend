using Goofy.Application.Core.DependencyInjection;
using Goofy.Application.PluggableCore.Abstractions;
using Goofy.Application.PluggableCore.Services;
using Goofy.Domain.PluggableCore.Service.Adapter;
using Goofy.Domain.PluggableCore.Service.Data;
using Goofy.Infrastructure.PluggableCore.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Goofy.Application.PluggableCore.DependencyInjection
{
    public static class GoofyApplicationPluginsServiceCollectionExtensions
    {
        public static IServiceCollection AddPluggableCore(this IServiceCollection services)
        {
            services.AddGoofyCore();
            services.AddSingleton<IGoofyAssemblyProvider, GoofyAssemblyProvider>();
            services.AddSingleton<IPluginAssemblyProvider, GoofyPluginAssemblyProvider>();
            services.AddSingleton<IPluginManager, PluginManager>();
            services.AddSingleton(typeof(IPluginServiceMapper<,>), typeof(PluginServiceMapper<,>));
            services.AddSingleton(typeof(IPluginUnitOfWork), typeof(PluginsContext));
            services.AddSingleton<IEngine, PluginBasedEngine>();
            return services;
        }
    }
}
