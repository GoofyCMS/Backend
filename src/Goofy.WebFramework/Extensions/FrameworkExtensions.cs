using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Goofy.Core.Infrastructure;
using Goofy.Core.Components.Base;

namespace Goofy.WebFramework.Extensions
{
    public static class FrameworkExtensions
    {
        public static void AddGoofyWebFramework(this IServiceCollection services)
        {
            services.AddOptions();
            services.AddWebGoofyCore();
            services.AddWebGoofyData();

            var componentsAssembliesProvider = services.Resolve<IComponentsAssembliesProvider>();
            services.AddMvc().AddControllersAsServices(componentsAssembliesProvider.ComponentsAssemblies);

            var engine = services.Resolve<IEngine>();
            engine.Start(services);
        }
    }
}
