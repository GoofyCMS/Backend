using Goofy.WebFramework;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.PlatformAbstractions;

namespace Goofy.WebApi
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
