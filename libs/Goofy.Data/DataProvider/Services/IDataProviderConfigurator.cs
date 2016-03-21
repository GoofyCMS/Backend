using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace Goofy.Data.DataProvider.Services
{
    public interface IDataProviderConfigurator
    {
        void ConfigureDbContextProvider(DbContextOptionsBuilder options);

        void AddDbContextObject<T>(EntityFrameworkServicesBuilder builder) where T : DbContext;

        void AddDataProvider(EntityFrameworkServicesBuilder builder);

        //EntitiFramework Dependencies

        void AddRelationalAnnotationProvider(IServiceCollection services);

        void AddMigrationsAnnotationProvider(IServiceCollection dependencyContainer);

        void AddRelationalTypeMapper(IServiceCollection dependencyContainer);

        void AddSqlGenerator(IServiceCollection dependencyContainer);

        //Relacionado con el generador de objetos C# que representan las consultas Sql
        void AddSqlCommandBuilderAndDependencies(IServiceCollection dependencyContainer);

        void AddMigrationsModelDiffer(IServiceCollection dependencyContainer);
    }
}
