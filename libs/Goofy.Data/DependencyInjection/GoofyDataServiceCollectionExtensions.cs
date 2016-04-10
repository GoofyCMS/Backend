using Goofy.Data;
using Goofy.Data.Components;
using Goofy.Data.DataProvider;
using Goofy.Data.DataProvider.Services;

using Goofy.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GoofyDataServiceCollectionExtensions
    {
        public static IServiceCollection AddGoofyData(this IServiceCollection services)
        {
            services.Configure<GoofyDataConfiguration>(c => { });
            services.AddSingleton<GoofyDataAccessManager>();
            var dataAccessManager = services.Resolve<GoofyDataAccessManager>();
            services.AddScoped(typeof(IEntityFrameworkDataProvider), dataAccessManager.DataProviderType);
            services.AddScoped(typeof(IDataProviderConfigurator), dataAccessManager.DataProviderConfiguratorType);

            var efServicesBuilder = services.AddEntityFramework();
            var dataProviderConfigurator = services.Resolve<IDataProviderConfigurator>();
            dataProviderConfigurator.AddDataProvider(efServicesBuilder);
            dataProviderConfigurator.AddRelationalAnnotationProvider(services);
            dataProviderConfigurator.AddMigrationsAnnotationProvider(services);
            dataProviderConfigurator.AddSqlCommandBuilderAndDependencies(services);
            dataProviderConfigurator.AddDbContextObject<ComponentContext>(efServicesBuilder);
            return services;
        }
    }
}
