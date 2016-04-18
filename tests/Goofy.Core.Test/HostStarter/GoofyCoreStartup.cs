using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Goofy.Core.Test.HostStarter
{
    public class GoofyCoreStartup
    {
        public GoofyCoreStartup()
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGoofyCore();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
        }

        public static void Main(string[] args) => WebApplication.Run<GoofyCoreStartup>(args);
    }
}
