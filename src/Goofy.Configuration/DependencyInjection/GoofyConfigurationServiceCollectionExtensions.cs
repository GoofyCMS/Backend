
using Microsoft.Extensions.DependencyInjection;

namespace Goofy.Configuration.DependencyInjection
{
    public static class GoofyConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddGoofyConfiguration(this IServiceCollection services)
        {
            services.Configure<DataAccessConfiguration>(cfg => { });
            return services;
        }
    }
}
