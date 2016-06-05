using Microsoft.Extensions.DependencyInjection;
using Goofy.Presentation.Core;
using Goofy.Application.PluggableCore.DependencyInjection;
using Goofy.Presentation.PluggableCore.Extensions;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Logging;
using Goofy.Presentation.PluggableCore.Providers;
using Goofy.Application.PluggableCore.Extensions;
using Microsoft.AspNet.Builder;
using Goofy.Security.UserModel;
using Goofy.Security.DependencyInjection;

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
            services.AddSecurity();
            services.AddMvcServices();
            services.StartEngine();
        }

        public override void Configure(IApplicationBuilder builder, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            base.Configure(builder, env, loggerFactory);
            if (!env.IsDevelopment())
            {
                builder.UseIdentity();
            }
        }
    }
}
