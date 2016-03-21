using System;

using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

using Goofy.Core.DependencyInjection.DesignTimeExtensions;
using Goofy.Core.Components.Base;

using Goofy.Data;
using Goofy.Data.DataProvider;
using Goofy.Data.DataProvider.Services;
using Goofy.Data.Context.Extensions;

using Goofy.WebFramework.Data.Components;
using System.Linq;
using Goofy.WebFramework.Data.Services;

namespace Goofy.WebFramework.Data.DependencyInjection
{
    public static class GoofyDataServiceCollectionExtensions
    {
        public static void AddGoofyData(this IServiceCollection services)
        {
            var builder = services.AddEntityFramework();
            services.AddGoofyData(builder);
        }

        private static void AddGoofyData(this IServiceCollection services, EntityFrameworkServicesBuilder builder)
        {
            services.AddSingleton(typeof(GoofyDataAccessManager));
            var goofyDataAccesMananager = services.Resolve<GoofyDataAccessManager>();

            services.AddScoped(typeof(IEntityFrameworkDataProvider), goofyDataAccesMananager.DataProviderType);
            services.AddScoped(typeof(IDataProviderConfigurator), goofyDataAccesMananager.DataProviderConfiguratorType);

            var dataProviderConfigurator = services.Resolve<IDataProviderConfigurator>();
            dataProviderConfigurator.AddDataProvider(builder);
            dataProviderConfigurator.AddRelationalAnnotationProvider(services);
            dataProviderConfigurator.AddMigrationsAnnotationProvider(services);
            dataProviderConfigurator.AddSqlCommandBuilderAndDependencies(services);
            dataProviderConfigurator.AddDbContextObject<ComponentContext>(builder);

            services.AddScoped(typeof(IComponentDbContextPopulator), typeof(GoofyWebComponentDbContextPopulator));
            var componentDbContextPopulator = services.Resolve<IComponentDbContextPopulator>();
            componentDbContextPopulator.PopulateComponentDbContext(services);
            var componentDbContextPopulatorDescriptor = services.Where(sd => sd.ServiceType == typeof(IComponentDbContextPopulator)).FirstOrDefault();
            services.Remove(componentDbContextPopulatorDescriptor);

            //var dataProviderServiceDesriptor = services.Where(sd => sd.ServiceType == typeof(IDataProviderConfigurator)).FirstOrDefault();
            //services.Remove(dataProviderServiceDesriptor);

        }

        public static void AddDbContextObject<T>(this IServiceCollection services) where T : DbContext
        {
            var efServiceBuilder = new EntityFrameworkServicesBuilder(services);
            var dataProviderConfigurator = services.Resolve<IDataProviderConfigurator>();
            dataProviderConfigurator.AddDbContextObject<T>(efServiceBuilder);
        }

        //private static void AddMigrationsStuff(this ServiceCollection services)
        //{
        //    services.AddSingleton<CSharpHelper>();
        //    services.AddSingleton<CSharpMigrationOperationGenerator>();
        //    services.AddSingleton<CSharpSnapshotGenerator>();
        //}
    }
}
