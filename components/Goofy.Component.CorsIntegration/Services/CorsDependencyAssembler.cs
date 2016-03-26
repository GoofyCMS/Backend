using Microsoft.Extensions.DependencyInjection;

using Goofy.Core.Infrastructure;
using Goofy.Configuration.Extensions;
using Goofy.Component.CorsIntegration.Configuration;
using Goofy.Component.CorsIntegration.CorsExtensions;

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

        public void Register(IServiceCollection services)
        {
            services.Configure<CorsConfiguration, CorsConfigConfigurator>();
            var corsConfig = services.GetConfiguration<CorsConfiguration>();
            services.AddPolicies(corsConfig);
        }
    }
}
