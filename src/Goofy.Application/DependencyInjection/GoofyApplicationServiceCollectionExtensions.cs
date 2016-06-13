using Goofy.Application.Core.Abstractions;
using Goofy.Security.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Goofy.Application.DependencyInjection
{
    public static class GoofyApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddGoofy(this IServiceCollection services)
        {
            services.AddSingleton<IGoofyAssemblyProvider, GoofyAssemblyProvider>();
            services.AddSingleton<IPluginAssemblyProvider, GoofyPluginAssemblyProvider>();
            services.AddInstance(services);
            services.AddSingleton<IEngine, PluggableEngine>();
            services.AddCrudPermissions("Plugin");
            return services;
        }

        public static void StartEngine(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var engine = serviceProvider.GetRequiredService<IEngine>();
            engine.Start();
        }

    }
}
