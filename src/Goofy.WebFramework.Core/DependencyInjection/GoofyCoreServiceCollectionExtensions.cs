using Goofy.Core.Components.Base;
using Goofy.WebFramework.Core.Components;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GoofyCoreServiceCollectionExtensions
    {
        public static void AddWebGoofyCore(this IServiceCollection services)
        {
            services.AddGoofyCore();
            services.Remove<IComponentsDirectoryPathProvider>(true);
            services.AddScoped<IComponentsDirectoryPathProvider, GoofyWebComponentsDirectoryPathProvider>();
        }
    }
}
