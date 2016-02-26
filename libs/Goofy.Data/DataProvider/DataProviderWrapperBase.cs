using System;
using System.Collections.Generic;

using Goofy.Core.Infrastructure;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Migrations.Internal;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Storage.Internal;

namespace Goofy.Data.DataProvider
{
    public abstract class DataProviderWrapperBase : IEntityFrameworkDataProviderWrapper
    {
        public virtual void AddDbContextObject<T>(EntityFrameworkServicesBuilder builder) where T : DbContext
        {
            builder.AddDbContext<T>(b => { ConfigureDbContextProvider(b); });
        }



        public abstract void AddDataProvider(EntityFrameworkServicesBuilder serviceBuilder);

        public abstract void ConfigureDbContextProvider(DbContextOptionsBuilder options);


        public void AddMigrationsModelDiffer(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.Register<MigrationsModelDiffer>();
            AddMigrationsAnnotationProvider(dependencyContainer);
            AddRelationalAnnotationProvider(dependencyContainer);
        }

        public abstract void AddMigrationsAnnotationProvider(IDependencyContainer dependencyContainer);

        public abstract void AddRelationalAnnotationProvider(IDependencyContainer dependencyContainer);



        public virtual void AddSqlCommandBuilderAndDependencies(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<SqlCommandBuilder, ISqlCommandBuilder>();
            dependencyContainer.RegisterDependency<ParameterNameGeneratorFactory, IParameterNameGeneratorFactory>();
            dependencyContainer.RegisterDependency<RelationalCommandBuilderFactory, IRelationalCommandBuilderFactory>();
            dependencyContainer.RegisterDependency(typeof(SensitiveDataLogger<>), typeof(ISensitiveDataLogger<>));
            AddSqlGenerator(dependencyContainer);
            AddRelationalTypeMapper(dependencyContainer);
        }

        public abstract void AddRelationalTypeMapper(IDependencyContainer dependencyContainer);

        public abstract void AddSqlGenerator(IDependencyContainer dependencyContainer);



        //Operaciones en la base de datos
        public abstract bool TablesExist(IDependencyContainer dependencyContainer, DatabaseFacade database, IEnumerable<string> tables);
    }
}
