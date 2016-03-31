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
            services.AddScoped<IAssembliesProvider, GoofyAssembliesProvider>();
            services.AddScoped<IResourcesLocator, GoofyResourcesLocator>();
            services.AddScoped<IComponentsDirectoryPathProvider, GoofyComponentsDirectoryPathProvider>();
            services.AddScoped<IComponentsConfigurationFileValidator, GoofyComponentConfigurationFileValidator>();
            //var assembliesProvider = 
            services.AddSingleton<IComponentsAssembliesProvider, GoofyComponentsAssembliesProvider>();
            services.AddScoped<IComponentsInfoProvider, GoofyComponentsInfoProvider>();
            services.AddScoped<IEngine, GoofyEngine>();
            return services;
        }
    }
}
