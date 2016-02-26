using System;
using System.Collections.Generic;

using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Internal;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Storage.Internal;
using Microsoft.Data.Entity.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

using Goofy.Core.Infrastructure;
using Microsoft.Data.Entity;

namespace Goofy.Data.DataProvider
{
    class SqlDataProviderWrapper : DataProviderWrapperBase
    {
        public override void AddDataProvider(EntityFrameworkServicesBuilder serviceBuilder)
        {
            serviceBuilder.AddSqlServer();
        }

        public override void AddMigrationsAnnotationProvider(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<SqlServerMigrationsAnnotationProvider, MigrationsAnnotationProvider>();
        }

        public override void AddRelationalAnnotationProvider(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<SqlServerAnnotationProvider, IRelationalAnnotationProvider>();
        }

        public override void AddRelationalTypeMapper(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<SqlServerTypeMapper, IRelationalTypeMapper>();
        }

        public override void AddSqlGenerator(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<SqlServerSqlGenerator, ISqlGenerator>();
        }

        public override void ConfigureDbContextProvider(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(GoofyDataAccessManager.GoofyDataConfiguration.DefaultConnection.ConnectionString);
        }

        public override bool TablesExist(IDependencyContainer dependencyContainer, DatabaseFacade database, IEnumerable<string> tables)
        {
            throw new NotImplementedException();
        }
    }
}
