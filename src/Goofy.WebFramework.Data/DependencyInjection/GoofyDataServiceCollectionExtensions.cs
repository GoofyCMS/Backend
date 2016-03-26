using Goofy.Data.DataProvider.Services;

using Goofy.WebFramework.Data;
using Goofy.WebFramework.Data.Components;
using Goofy.WebFramework.Data.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GoofyDataServiceCollectionExtensions
    {
        public static void AddWebGoofyData(this IServiceCollection services)
        {
            services.AddGoofyData();

            var builder = services.AddEntityFramework();
            var dataProviderConfigurator = services.Resolve<IDataProviderConfigurator>();
            dataProviderConfigurator.AddDbContextObject<ComponentContext>(builder);

            services.AddScoped<IComponentDbContextPopulator, GoofyWebComponentDbContextPopulator>();
            var componentDbContextPopulator = services.Resolve<IComponentDbContextPopulator>();
            componentDbContextPopulator.PopulateComponentDbContext(services);
            services.Remove<IComponentDbContextPopulator>();
        }
    }
}
