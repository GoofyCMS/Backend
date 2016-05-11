using Microsoft.Extensions.DependencyInjection;
using Goofy.Domain.Core.Abstractions;

namespace Goofy.Application.Core.DependencyInjection
{
    public static class GoofyApplicationCoreServiceCollectionExtensions
    {
        public static IServiceCollection AddGoofyCore(this IServiceCollection services)
        {
            services.AddInstance(services);
            services.AddScoped<IGoofyAssemblyProvider, GoofyAssemblyProvider>();
            services.AddSingleton<IEngine, GoofyEngine>();
            return services;
        }
    }
}
