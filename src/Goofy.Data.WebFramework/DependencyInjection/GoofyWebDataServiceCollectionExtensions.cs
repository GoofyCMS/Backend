using Goofy.Data.DataProvider.Services;
using Goofy.Data.WebFramework.Components;
using Goofy.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GoofyWebDataServiceCollectionExtensions
    {
        public static void AddWebGoofyData(this IServiceCollection services)
        {
            services.AddGoofyData();

            var builder = services.AddEntityFramework();
            var dataProviderConfigurator = services.Resolve<IDataProviderConfigurator>();
            dataProviderConfigurator.AddDbContextObject<ComponentContext>(builder);
        }
    }
}
