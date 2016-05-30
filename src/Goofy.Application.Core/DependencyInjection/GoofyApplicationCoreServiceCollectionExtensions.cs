using Microsoft.Extensions.DependencyInjection;
using Goofy.Infrastructure.Core.Data.Configuration;

namespace Goofy.Application.Core.DependencyInjection
{
    public static class GoofyApplicationCoreServiceCollectionExtensions
    {
        public static IServiceCollection AddGoofyCore(this IServiceCollection services)
        {
            services.AddInstance(services);
            services.Configure<DataAccessConfiguration>(_ => { });
            return services;
        }
    }
}
