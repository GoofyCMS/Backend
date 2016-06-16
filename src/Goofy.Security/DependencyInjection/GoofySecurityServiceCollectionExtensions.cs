using Microsoft.Extensions.DependencyInjection;
using Goofy.Security.Extensions;
using Goofy.Security.Services;

namespace Goofy.Security.DependencyInjection
{
    public static class GoofySecurityServiceCollectionExtensions
    {
        public static IServiceCollection AddGoofySecurity(this IServiceCollection services)
        {
            services.BuildPermissions();
            services.AddScoped<CustomRequireClaimService>();
            services.AddScoped<AuthorizationService>();
            return services;
        }
    }
}