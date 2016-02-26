using System.Collections.Generic;
using System.Linq;

using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Internal;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Storage.Internal;
using Microsoft.Data.Entity.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

using Goofy.Core.Infrastructure;

namespace Goofy.Data.DataProvider
{
    public class SqliteDataProvider : DataProviderWrapperBase
    {
        public override void AddDataProvider(EntityFrameworkServicesBuilder serviceBuilder)
        {
            serviceBuilder.AddSqlite();
        }

        public override void AddMigrationsAnnotationProvider(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<SqliteMigrationsAnnotationProvider, IMigrationsAnnotationProvider>();
        }

        public override void AddRelationalAnnotationProvider(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<SqliteAnnotationProvider, IRelationalAnnotationProvider>();
        }

        public override void AddRelationalTypeMapper(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<SqliteTypeMapper, IRelationalTypeMapper>();
        }

        public override void AddSqlGenerator(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<SqliteSqlGenerator, ISqlGenerator>();
        }


        public override bool TablesExist(IDependencyContainer container, DatabaseFacade database, IEnumerable<string> tables)
        {
            string query = "SELECT COUNT(*) FROM sqlite_master AS t WHERE \"type\" = 'table' AND ({0})";

            string orStatements = "";
            var tablesArray = tables.ToArray();

            for (int i = 0; i < tablesArray.Length; i++)
            {
                orStatements += "t.name == {" + i + "} OR ";
            }
            orStatements = orStatements.Substring(0, orStatements.Length - 4);

            //Armar la consulta y agregar el ; al final de la sentencia
            query = string.Format(query, orStatements);
            query += ";";

            //Ejecutar la consulta
            var commandBuilder = (SqlCommandBuilder)container.Resolve(typeof(SqlCommandBuilder));

            var command = commandBuilder.Build(query, tablesArray);
            using (var t = database.BeginTransaction())
            {
                var count = (long)command.ExecuteScalar(t.Connection);
                return count == tablesArray.Length;
            }
        }

        public override void ConfigureDbContextProvider(DbContextOptionsBuilder options)
        {
            options.UseSqlite(GoofyDataAccessManager.GoofyDataConfiguration.DefaultConnection.ConnectionString);
        }
    }
}
