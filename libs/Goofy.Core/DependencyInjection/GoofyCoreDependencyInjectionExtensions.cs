using Microsoft.Extensions.Configuration;

using Goofy.Core.Configuration;
using Goofy.Core.Infrastructure;
using Goofy.Core.Components;
using Goofy.Core.Components.Base;
using Goofy.Extensions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GoofyCoreDependencyInjectionExtensions
    {
        private static GoofyInMemoryComponentStore componentStore;

        public static IServiceCollection AddGoofyCore(this IServiceCollection services)
        {
            /*
                Allow components can resolve IServiceCollection at framework load time,
                it'll be removed when calling GoofyEngine.Start()
            */
            services.AddInstance<ConfigurationBuilder>(new DesignTimeConfigurationBuilder());
            services.AddInstance(services);
            services.AddOptions();
            services.Configure<GoofyCoreConfiguration>(c => { });
            services.AddScoped<IAssembliesProvider, GoofyAssembliesProvider>();
            services.AddScoped<IResourcesLocator, GoofyResourcesLocator>();
            services.AddScoped<IComponentsDirectoryPathProvider, GoofyComponentsDirectoryPathProvider>();
            services.AddScoped<IComponentsConfigurationFileValidator, GoofyComponentConfigurationFileValidator>();
            services.AddSingleton<IComponentsAssembliesProvider, GoofyComponentsAssembliesProvider>();
            services.AddScoped<IComponentsInfoProvider, GoofyComponentsInfoProvider>();
            services.AddScoped<IComponentStorePersist<GoofyInMemoryComponentStore>, GoofyInMemoryComponentStoreManager>();
            services.AddScoped<IComponentStoreStarter<GoofyInMemoryComponentStore>, GoofyInMemoryComponentStoreManager>();
            services.AddSingleton<GoofyInMemoryComponentStore>();
            services.AddSingleton(ComponentStoreFactory);
            services.AddSingleton<IEngine, GoofyEngine>();
            return services;
        }

        private static IComponentStore ComponentStoreFactory(IServiceProvider serviceProvider)
        {
            if (componentStore == null)
                componentStore = serviceProvider.GetService<GoofyInMemoryComponentStore>();

            return componentStore;
        }
    }
}
