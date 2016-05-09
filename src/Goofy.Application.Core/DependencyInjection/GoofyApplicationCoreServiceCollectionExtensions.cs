using Microsoft.Extensions.DependencyInjection;
using Goofy.Domain.Core.Abstractions;

namespace Goofy.Application.Core.DependencyInjection
{
    public static class GoofyApplicationCoreServiceCollectionExtensions
    {
        //public static object ServiceMapperFactory(IServiceProvider serviceProvider, Type serviceType)
        //{
        //}

        public static IServiceCollection AddGoofyCore(this IServiceCollection services)
        {
            services.AddInstance(services);
            services.AddSingleton<IPluginAssemblyProvider, GoofyPluginAssemblyProvider>();
            services.AddScoped<IGoofyAssemblyProvider, GoofyAssemblyProvider>();

            //services.AddSingleton<ITypeAdapterFactory, AutoMapperTypeAdapterFactory>();
            services.AddSingleton<IEngine, GoofyEngine>();
            return services;
        }
    }
}
