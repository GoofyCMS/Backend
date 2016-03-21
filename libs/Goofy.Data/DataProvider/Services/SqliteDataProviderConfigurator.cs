using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Internal;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Storage.Internal;

using Microsoft.Extensions.DependencyInjection;

namespace Goofy.Data.DataProvider.Services
{
    public class SqliteDataProviderConfigurator : DataProviderConfiguratorBase
    {
        public SqliteDataProviderConfigurator(GoofyDataAccessManager dataAccessManager)
            : base(dataAccessManager)
        {
        }

        public override void AddDataProvider(EntityFrameworkServicesBuilder serviceBuilder)
        {
            serviceBuilder.AddSqlite();
        }

        public override void AddMigrationsAnnotationProvider(IServiceCollection services)
        {
            services.AddScoped(typeof(IMigrationsAnnotationProvider), typeof(SqliteMigrationsAnnotationProvider));
        }

        public override void AddRelationalAnnotationProvider(IServiceCollection services)
        {
            services.AddScoped(typeof(IRelationalAnnotationProvider), typeof(SqliteAnnotationProvider));
        }

        public override void AddRelationalTypeMapper(IServiceCollection services)
        {
            services.AddScoped(typeof(IRelationalTypeMapper), typeof(SqliteTypeMapper));
        }

        public override void AddSqlGenerator(IServiceCollection services)
        {
            services.AddScoped(typeof(ISqlGenerator), typeof(SqliteSqlGenerator));
        }

        public override void ConfigureDbContextProvider(DbContextOptionsBuilder options)
        {
            options.UseSqlite(_dataAccesManager.GoofyDataConfiguration.DefaultConnection.ConnectionString);
        }
    }
}
