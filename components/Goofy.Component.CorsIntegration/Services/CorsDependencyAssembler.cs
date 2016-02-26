using Microsoft.Extensions.Configuration;

using Goofy.Configuration.Extensions;
using Goofy.Core.Infrastructure;
using Goofy.WebFramework.Infrastructure;

using Goofy.Component.CorsIntegration.CorsExtensions;
using Goofy.Component.CorsIntegration.Configuration;

namespace Goofy.Component.CorsIntegration.Services
{
    public class CorsDependencyAssembler : IGoofyDependencyAssembler
    {
        public int Order
        {
            get
            {
                return 0;
            }
        }

        public void Register(IDependencyContainer builder, IResourcesLoader loader)
        {
        }

        public void RegisterWebDependencies(IWebDependencyContainer container, IResourcesLoader loader, IConfiguration config)
        {
            var configSection = config.GetSection("Goofy.Component.CorsIntegration");
            container.RegisterConfigurations<CorsConfiguration>(configSection);
            var corsConfig = configSection.GetConfiguration<CorsConfiguration>();
            container.AddPolicies(corsConfig);
        }
    }
}
