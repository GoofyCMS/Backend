using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Internal;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Storage.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Goofy.Data.DataProvider.Services
{
    public class SqlDataProviderConfigurator : DataProviderConfiguratorBase
    {
        public SqlDataProviderConfigurator(GoofyDataAccessManager dataAccessManager) : base(dataAccessManager)
        {
        }

        public override void AddDataProvider(EntityFrameworkServicesBuilder serviceBuilder)
        {
            serviceBuilder.AddSqlServer();
        }

        public override void AddMigrationsAnnotationProvider(IServiceCollection services)
        {
            services.AddScoped(typeof(MigrationsAnnotationProvider), typeof(SqlServerMigrationsAnnotationProvider));
        }

        public override void AddRelationalAnnotationProvider(IServiceCollection services)
        {
            services.AddScoped(typeof(IRelationalAnnotationProvider), typeof(SqlServerAnnotationProvider));
        }

        public override void AddRelationalTypeMapper(IServiceCollection services)
        {
            services.AddScoped(typeof(IRelationalTypeMapper), typeof(SqlServerTypeMapper));
        }

        public override void AddSqlGenerator(IServiceCollection services)
        {
            services.AddScoped(typeof(ISqlGenerator), typeof(SqlServerSqlGenerator));
        }

        public override void ConfigureDbContextProvider(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_dataAccesManager.GoofyDataConfiguration.DefaultConnection.ConnectionString);
        }


    }
}
