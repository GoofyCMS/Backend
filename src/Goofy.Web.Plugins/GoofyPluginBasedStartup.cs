using Microsoft.Extensions.DependencyInjection;
using Goofy.Web.Core;
using Goofy.Application.Plugins.DependencyInjection;
using Goofy.Application.Core.Extensions;
using Goofy.Web.Plugins.Extensions;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Logging;
using Goofy.Web.Plugins.Providers;

namespace Goofy.Web.Plugins
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
            services.AddPluginBasedCore();
            services.AddSingleton<PluginContextProvider>();
            services.AddMvcServices();
            services.StartEngine();
        }
    }
}
