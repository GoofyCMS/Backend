using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Goofy.Web.Core;

namespace Goofy.Web
{
    public class Startup : GoofyStartup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment app)
            : base(env, app)
        {
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
