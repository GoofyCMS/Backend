using Microsoft.Extensions.DependencyInjection;
using Goofy.Security.Extensions;

namespace Goofy.Security.DependencyInjection
{
    public static class GoofySecurityServiceCollectionExtensions
    {
        public static IServiceCollection AddGoofySecurity(this IServiceCollection services)
        {
            services.BuildPermissions();
            return services;
        }
    }
}
