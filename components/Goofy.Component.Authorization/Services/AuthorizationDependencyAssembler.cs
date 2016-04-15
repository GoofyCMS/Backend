using Microsoft.Extensions.DependencyInjection;

using Goofy.Core.Infrastructure;
using Goofy.Component.Authorization.Resources;
using Goofy.Component.Authorization.Configuration;

namespace Goofy.Component.Authorization.Services
{
    public class AuthorizationDependencyAssembler : IDependencyAssembler
    {
        public int Order
        {
            get
            {
                return 0;
            }
        }

        public void Register(IServiceCollection services)
        {
            services.ConfigureComponentConfigurationFile<AuthorizationConfiguration>("Goofy.Component.Authorization");
            services.AddScoped<IGoofyClaimManager, GoofyClaimManager>();
            services.AddScoped<IGoofyCrudPoliciesManager, GoofyCrudPoliciesManager>();
        }
    }
}
