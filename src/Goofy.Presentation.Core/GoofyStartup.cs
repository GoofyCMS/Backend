using Microsoft.Extensions.DependencyInjection;
using Goofy.Configuration.DependencyInjection;

namespace Goofy.Presentation.Core
{
    public class GoofyStartup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddInstance(services);
            services.AddGoofyConfiguration();
        }
    }
}
