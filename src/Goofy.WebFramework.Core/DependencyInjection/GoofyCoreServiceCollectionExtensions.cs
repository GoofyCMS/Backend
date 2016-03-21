using Microsoft.Extensions.DependencyInjection;

using Goofy.Core.Components;
using Goofy.Core.Components.Base;
using Goofy.Core.Infrastructure;

using Goofy.WebFramework.Core.Infrastructure;
using Goofy.WebFramework.Core.Components;

namespace Goofy.WebFramework.Core.DependencyInjection
{
    public static class GoofyCoreServiceCollectionExtensions
    {
        public static void AddGoofyCore(this IServiceCollection services)
        {
            services.AddScoped(typeof(IResourcesLocator), typeof(GoofyDomainResourcesLocator));
            services.AddScoped(typeof(IResourcesLoader), typeof(GoofyResourcesLoader));
            services.AddSingleton(typeof(IComponentsAssembliesProvider), typeof(GoofyComponentsAssembliesProvider));
            services.AddScoped(typeof(IComponentsDirectoryPathProvider), typeof(GoofyWebComponentsDirectoryPathProvider));
            services.AddScoped(typeof(IComponentsConfigurationFileValidator), typeof(GoofyComponentConfigurationFileValidator));
            services.AddScoped(typeof(IComponentsInfoProvider), typeof(GoofyComponentsInfoProvider));
            //No usar este objeto más, contenía demasiadas funcionalidades que se dividieron en servicios
            //services.AddSingleton(typeof(GoofyComponentManager), typeof(GoofyWebComponentManager));
            services.AddSingleton(typeof(GoofyEngine), typeof(GoofyWebEngine));
        }
    }
}
