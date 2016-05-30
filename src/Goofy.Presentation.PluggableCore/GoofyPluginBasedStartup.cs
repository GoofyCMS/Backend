using Microsoft.Extensions.DependencyInjection;
using Goofy.Presentation.Core;
using Goofy.Application.PluggableCore.DependencyInjection;
using Goofy.Presentation.PluggableCore.Extensions;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Logging;
using Goofy.Presentation.PluggableCore.Providers;
using Goofy.Application.PluggableCore.Extensions;

namespace Goofy.Presentation.PluggableCore
{
    public class GoofyPluginBasedStartup : GoofyStartup
    {
        public GoofyPluginBasedStartup(IHostingEnvironment env, IApplicationEnvironment app, ILoggerFactory loggerFactory)
            : base(env, app, loggerFactory)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            LoggerFactory.AddConsole(Configuration.GetSection("Logging"));
            services.AddPluggableCore();
            services.AddSingleton<PluginContextProvider>();
            services.AddMvcServices();
            services.StartEngine();
        }
    }
}
