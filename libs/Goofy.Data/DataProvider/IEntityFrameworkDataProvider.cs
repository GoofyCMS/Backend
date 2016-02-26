using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

using Goofy.Core.Infrastructure;
using System.Collections.Generic;

namespace Goofy.Data.DataProvider
{
    public interface IEntityFrameworkDataProviderWrapper
    {
        void ConfigureDbContextProvider(DbContextOptionsBuilder options);

        void AddDbContextObject<T>(EntityFrameworkServicesBuilder builder) where T : DbContext;

        void AddDataProvider(EntityFrameworkServicesBuilder builder);

        //EntitiFramework Dependencies

        void AddRelationalAnnotationProvider(IDependencyContainer dependencyContainer);

        void AddMigrationsAnnotationProvider(IDependencyContainer dependencyContainer);

        void AddRelationalTypeMapper(IDependencyContainer dependencyContainer);

        void AddSqlGenerator(IDependencyContainer dependencyContainer);

        //Relacionado con el generador de objetos C# que representan las consultas Sql
        void AddSqlCommandBuilderAndDependencies(IDependencyContainer dependencyContainer);

        void AddMigrationsModelDiffer(IDependencyContainer dependencyContainer);

        //Consultas del proveedor de datos
        bool TablesExist(IDependencyContainer dependencyContainer, DatabaseFacade database, IEnumerable<string> tables);
    }
}
