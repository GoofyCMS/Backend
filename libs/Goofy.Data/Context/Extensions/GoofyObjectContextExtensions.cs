using Goofy.Core.Infrastructure;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Internal;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Migrations.Operations;
using System.Collections.Generic;

namespace Goofy.Data.Context.Extensions
{
    public static class GoofyObjectContextExtensions
    {
        public static void CreateTablesIfNotExists(this DbContext dbContext, IDependencyContainer container)
        {
            var modelDiffer = (MigrationsModelDiffer)container.Resolve(typeof(MigrationsModelDiffer));
            var sqlGenerator = (MigrationsSqlGenerator)container.Resolve(typeof(MigrationsSqlGenerator));

            var upOperations = modelDiffer.GetDifferences(null, dbContext.Model);

            /*
                Esto se hizo porque dio error de compilación luego de pasar de dnx451 a net451 el siguiente código:
                var tables = upOperations.Where(o => o is CreateTableOperation).Cast<CreateTableOperation>().Select(o => o.Name);
            */
            var tables = new List<string>();
            foreach (var operation in upOperations)
            {
                var createOperation = operation as CreateTableOperation;
                if(createOperation != null)
                    tables.Add(createOperation.Name);
            }

            if (!GoofyDataAccessManager.GoofyDataConfiguration.Provider.TablesExist(container, dbContext.Database, tables))// alguna tabla fue eliminada
            {
                dbContext.DropTables(container);

                //Crear nuevamente las tablas
                var sqlOperations = sqlGenerator.Generate(upOperations);

                using (var t = dbContext.Database.BeginTransaction())
                {
                    sqlOperations.ExecuteNonQuery(t.Connection);
                    t.Commit();
                }
            }
        }

        public static void DropTables(this DbContext dbContext, IDependencyContainer container)
        {
            var modelDiffer = (MigrationsModelDiffer)container.Resolve(typeof(MigrationsModelDiffer));
            var sqlGenerator = (MigrationsSqlGenerator)container.Resolve(typeof(MigrationsSqlGenerator));

            var downOperations = modelDiffer.GetDifferences(dbContext.Model, null);
            var sqlDownOperations = sqlGenerator.Generate(downOperations);

            using (var t = dbContext.Database.BeginTransaction())
            {
                foreach (var operation in sqlDownOperations)
                {
                    try
                    {
                        operation.ExecuteNonQuery(t.Connection);
                    }
                    catch
                    {
                    }
                }
                t.Commit();
            }
        }
    }

}
