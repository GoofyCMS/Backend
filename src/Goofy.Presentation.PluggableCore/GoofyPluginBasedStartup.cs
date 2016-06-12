using Microsoft.Extensions.DependencyInjection;
using Goofy.Presentation.Core;
using Goofy.Application.PluggableCore.DependencyInjection;
using Goofy.Presentation.PluggableCore.Extensions;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Logging;
using Goofy.Application.PluggableCore.Extensions;
using Microsoft.AspNet.Builder;

namespace Goofy.Presentation.PluggableCore
{
    public class GoofyPluginBasedStartup : GoofyStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.AddPluggableCore();
            services.AddMvcServices();
            services.StartEngine();
        }

        public override void Configure(IApplicationBuilder builder, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            base.Configure(builder, env, loggerFactory);
        }
    }
}
