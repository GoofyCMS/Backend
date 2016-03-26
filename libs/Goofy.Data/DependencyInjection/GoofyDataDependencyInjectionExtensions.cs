using Microsoft.Data.Entity.Infrastructure;

using Goofy.Data;
using Goofy.Data.DataProvider;
using Goofy.Data.DataProvider.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GoofyDataDependencyInjectionExtensions
    {
        public static IServiceCollection AddGoofyData(this IServiceCollection services)
        {
            services.Configure<GoofyDataConfiguration>(c => { });
            services.AddSingleton<GoofyDataAccessManager>();
            var dataAccessManager = services.Resolve<GoofyDataAccessManager>();
            services.AddScoped(typeof(IEntityFrameworkDataProvider), dataAccessManager.DataProviderType);
            services.AddScoped(typeof(IDataProviderConfigurator), dataAccessManager.DataProviderConfiguratorType);

            var efServicesBuilder = new EntityFrameworkServicesBuilder(services);
            var dataProviderConfigurator = services.Resolve<IDataProviderConfigurator>();
            dataProviderConfigurator.AddDataProvider(efServicesBuilder);
            dataProviderConfigurator.AddRelationalAnnotationProvider(services);
            dataProviderConfigurator.AddMigrationsAnnotationProvider(services);
            dataProviderConfigurator.AddSqlCommandBuilderAndDependencies(services);
            return services;
        }
    }
}
