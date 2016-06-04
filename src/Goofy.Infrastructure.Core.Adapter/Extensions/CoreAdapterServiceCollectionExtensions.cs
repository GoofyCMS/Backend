using System.Reflection;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Goofy.Domain.Core.Service.Adapter;

namespace Goofy.Infrastructure.Core.Adapter.Extensions
{
    public static class CoreAdapterServiceCollectionExtensions
    {
        public static IServiceCollection AddTypeAdpaterServices(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddInstance<ITypeAdapterFactory>(new AutomapperTypeAdapterFactory(assemblies));

            //Register TypeAdapter implementation
            services.AddSingleton(provider => provider.GetRequiredService<ITypeAdapterFactory>().Create());
            return services;
        }
    }
}
