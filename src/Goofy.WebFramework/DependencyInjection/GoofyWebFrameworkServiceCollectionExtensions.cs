using Goofy.Core.Infrastructure;
using Goofy.Core.Components.Base;
using Goofy.Core.WebFramework.DependencyInjection;

using Goofy.Extensions;
using Goofy.WebFramework.Mvc.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GoofyWebFrameworkServiceCollectionExtensions
    {
        public static void AddGoofyWebFramework(this IServiceCollection services)
        {
            services.AddWebGoofyCore();
            services.AddWebGoofyData();

            var componentsAssembliesProvider = services.Resolve<IComponentsAssembliesProvider>();
            services.AddMvc().AddControllersAsServices(componentsAssembliesProvider.ComponentsAssemblies);
            services.AddWebGoofyMvc();

            var engine = services.Resolve<IEngine>();
            engine.Start();
        }
    }
}
