using Microsoft.Extensions.DependencyInjection;

using Goofy.Core.Infrastructure;
using Goofy.Core.Entity.Base;
using Goofy.Component.Authorization.Resources;
using Goofy.Component.Authorization.Model;
using System.Collections.Generic;
using Goofy.Component.Authorization.DependencyInjection;

namespace Goofy.Component.Authorization
{
    public class BasicPermissionsStarter : IRunAtStartup
    {
        private readonly IServiceCollection _services;
        private readonly IResourcesLocator _resourcesLocator;

        public BasicPermissionsStarter(IServiceCollection services,
                                       IResourcesLocator resourcesLocator)
        {
            _services = services;
            _resourcesLocator = resourcesLocator;
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
            var policies = new List<GoofyCrudPolicy>();
            _services.AddGoofyCrudPolicies(_resourcesLocator.FindClassesOfType<GoofyEntityBase>());
        }
    }
}
