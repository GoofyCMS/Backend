using Microsoft.Extensions.DependencyInjection;

using Goofy.Core.Infrastructure;
using Goofy.Core.Components.Base;
using Goofy.Core.WebFramework.DependencyInjection;

using Goofy.Extensions;

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
