using Goofy.Extensions;
using Goofy.Core.Components.Base;
using Goofy.Data.WebFramework.Components;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GoofyWebDataServiceCollectionExtensions
    {
        public static void AddWebGoofyData(this IServiceCollection services)
        {
            services.AddGoofyData();
            services.Remove<IComponentStore>(true);
            services.AddScoped<IComponentStore, ComponentStore>();

            services.AddScoped<IComponentStoreStarter<ComponentStore>, ComponentStoreStarter>();
        }
    }
}
