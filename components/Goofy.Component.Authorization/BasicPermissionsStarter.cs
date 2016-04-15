using Microsoft.Extensions.DependencyInjection;

using Goofy.Core.Infrastructure;
using Goofy.Core.Entity.Base;
using Goofy.Component.Authorization.Resources;

namespace Goofy.Component.Authorization
{
    public class BasicPermissionsStarter : IRunAtStartup
    {
        private readonly IServiceCollection _services;
        private readonly IResourcesLocator _resourcesLocator;
        private readonly IPolicyAndClaimNameProvider _namesProvider;

        public BasicPermissionsStarter(IServiceCollection services,
                                       IResourcesLocator resourcesLocator,
                                       IPolicyAndClaimNameProvider namesProvider)
        {
            _services = services;
            _resourcesLocator = resourcesLocator;
            _namesProvider = namesProvider;
        }

        public int Order
        {
            get
            {
                return 0;
            }
        }

        public void Run()
        {
            foreach (var type in _resourcesLocator.FindClassesOfType<GoofyEntityBase>())
            {
                _services.AddAuthorization(options =>
                {
                    options.AddPolicy(_namesProvider.GetCreatePolicy(type), policy => policy.RequireClaim(_namesProvider.GetCreateClaim(type)));
                    options.AddPolicy(_namesProvider.GetUpdatePolicy(type), policy => policy.RequireClaim(_namesProvider.GetUpdateClaim(type)));
                    options.AddPolicy(_namesProvider.GetDeletePolicy(type), policy => policy.RequireClaim(_namesProvider.GetDeleteClaim(type)));
                });
            }

        }
    }
}
