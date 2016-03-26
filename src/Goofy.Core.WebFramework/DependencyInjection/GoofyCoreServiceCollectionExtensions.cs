using Microsoft.Extensions.DependencyInjection;

using Goofy.Core.Components.Base;
using Goofy.Core.WebFramework.Components;
using Goofy.Extensions;

namespace Goofy.Core.WebFramework.DependencyInjection
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
