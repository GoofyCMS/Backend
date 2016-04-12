using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Cors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;

using Goofy.Core.Infrastructure;
using Goofy.Component.CorsIntegration.Configuration;

namespace Goofy.Component.CorsIntegration
{
    public class GoofyCorsConfigurator : IRunAtStartup
    {
        private readonly IServiceCollection _services;
        private readonly CorsConfiguration _corsConfiguration;

        public GoofyCorsConfigurator(IServiceCollection services, IOptions<CorsConfiguration> corsConfigOptions)
        {
            _services = services;
            _corsConfiguration = corsConfigOptions.Value;
        }

        public int Order
        {
            get
            {
                return 1;
            }
        }

        public void Run()
        {
            _services.Configure<MvcOptions>(options =>
            {
                foreach (var policy in _corsConfiguration.Policies)
                    options.Filters.Add(new CorsAuthorizationFilterFactory(policy.Name));
            });
        }
    }
}
