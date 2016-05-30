using Microsoft.AspNet.Hosting;
using Goofy.Presentation.PluggableCore;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Logging;

namespace Goofy.Presentation
{
    public class Startup : GoofyPluginBasedStartup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment app, ILoggerFactory loggerFactory)
            : base(env, app, loggerFactory)
        {

        }
        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
