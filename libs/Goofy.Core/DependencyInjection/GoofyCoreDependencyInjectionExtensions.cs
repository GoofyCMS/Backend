using Goofy.Core.Components;
using Goofy.Core.Components.Base;
using Goofy.Core.Configuration;
using Goofy.Core.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GoofyCoreDependencyInjectionExtensions
    {
        public static IServiceCollection AddGoofyCore(this IServiceCollection services)
        {
            /*
                Allow components can resolve IServiceCollection at framework load time,
                it'll be removed when calling GoofyEngine.Start()
            */
            services.AddInstance(services); 
            services.AddOptions();
            services.Configure<GoofyCoreConfiguration>(c => { });
            services.AddScoped<IResourcesLocator, GoofyDomainResourcesLocator>();
            services.AddScoped<IResourcesLoader, GoofyResourcesLoader>();
            services.AddScoped<IComponentsDirectoryPathProvider, GoofyComponentsDirectoryPathProvider>();
            services.AddScoped<IComponentsConfigurationFileValidator, GoofyComponentConfigurationFileValidator>();
            services.AddScoped<IComponentsAssembliesProvider, GoofyComponentsAssembliesProvider>();
            services.AddScoped<IComponentsInfoProvider, GoofyComponentsInfoProvider>();
            services.AddScoped<IEngine, GoofyEngine>();
            return services;
        }
    }
}
