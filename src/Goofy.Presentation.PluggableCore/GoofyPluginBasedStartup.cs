using Microsoft.Extensions.DependencyInjection;
using Goofy.Presentation.Core;
using Goofy.Application.PluggableCore.DependencyInjection;
using Goofy.Presentation.PluggableCore.Extensions;
using Goofy.Application.PluggableCore.Extensions;
using Goofy.Security.DependencyInjection;

namespace Goofy.Presentation.PluggableCore
{
    public class GoofyPluginBasedStartup : GoofyStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.AddGoofySecurity();
            services.AddPluggableCore();
            services.AddMvcServices();
            services.StartEngine();
        }
    }
}
