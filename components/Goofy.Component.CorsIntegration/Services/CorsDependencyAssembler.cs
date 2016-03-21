using Microsoft.Extensions.Configuration;

using Goofy.Configuration.Extensions;
using Goofy.Core.Infrastructure;

using Goofy.Component.CorsIntegration.CorsExtensions;
using Goofy.Component.CorsIntegration.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Goofy.Component.CorsIntegration.Services
{
    public class CorsDependencyAssembler : IDependencyAssembler
    {
        public int Order
        {
            get
            {
                return 0;
            }
        }

        public void Register(IServiceCollection services, IResourcesLoader loader)
        {
            //throw new NotImplementedException();
        }

        //public void RegisterWebDependencies(IWebDependencyContainer container, IResourcesLoader loader, IConfiguration config)
        //{
        //    var configSection = config.GetSection("Goofy.Component.CorsIntegration");
        //    container.RegisterConfigurations<CorsConfiguration>(configSection);
        //    var corsConfig = configSection.GetConfiguration<CorsConfiguration>();
        //    container.AddPolicies(corsConfig);
        //}
    }
}
