using Goofy.Data.DataProvider.Services;

using Goofy.Data.WebFramework;
using Goofy.Data.WebFramework.Components;
using Goofy.Data.WebFramework.Services;

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
