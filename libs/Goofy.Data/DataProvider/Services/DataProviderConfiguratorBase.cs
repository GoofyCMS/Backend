using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Migrations.Internal;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Storage.Internal;

using Microsoft.Extensions.DependencyInjection;

namespace Goofy.Data.DataProvider.Services
{
    public abstract class DataProviderConfiguratorBase : IDataProviderConfigurator
    {
        protected readonly GoofyDataAccessManager _dataAccesManager;

        public DataProviderConfiguratorBase(GoofyDataAccessManager dataAccessManager)
        {
            _dataAccesManager = dataAccessManager;
        }

        public virtual void AddDbContextObject<T>(EntityFrameworkServicesBuilder builder) where T : DbContext
        {
            builder.AddDbContext<T>(b => { ConfigureDbContextProvider(b); });
        }

        public abstract void AddDataProvider(EntityFrameworkServicesBuilder serviceBuilder);

        public abstract void ConfigureDbContextProvider(DbContextOptionsBuilder options);


        public void AddMigrationsModelDiffer(IServiceCollection services)
        {
            services.AddScoped<MigrationsModelDiffer>();
            AddMigrationsAnnotationProvider(services);
            AddRelationalAnnotationProvider(services);
        }

        public abstract void AddMigrationsAnnotationProvider(IServiceCollection services);

        public abstract void AddRelationalAnnotationProvider(IServiceCollection services);



        public virtual void AddSqlCommandBuilderAndDependencies(IServiceCollection services)
        {
            services.AddScoped(typeof(ISqlCommandBuilder), typeof(SqlCommandBuilder));
            services.AddScoped(typeof(IParameterNameGeneratorFactory), typeof(ParameterNameGeneratorFactory));
            services.AddScoped(typeof(IRelationalCommandBuilderFactory), typeof(RelationalCommandBuilderFactory));
            services.AddScoped(typeof(ISensitiveDataLogger<>), typeof(SensitiveDataLogger<>));
            AddSqlGenerator(services);
            AddRelationalTypeMapper(services);
        }

        public abstract void AddRelationalTypeMapper(IServiceCollection dependencyContainer);

        public abstract void AddSqlGenerator(IServiceCollection dependencyContainer);



    }
}
