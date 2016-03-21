using System;

using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Migrations.Operations;

using Goofy.Data.DataProvider;
using System.Linq;

namespace Goofy.Data.Context.Extensions
{
    public static class GoofyObjectContextExtensions
    {
        public static void CreateTablesIfNotExists(this DbContext dbContext,
                                                        IMigrationsModelDiffer modelDiffer,
                                                        IMigrationsSqlGenerator sqlGenerator,
                                                        IEntityFrameworkDataProvider dataProvider)
        {
            var upOperations = modelDiffer.GetDifferences(null, dbContext.Model);

            var tables = upOperations.Where(o => o is CreateTableOperation).Cast<CreateTableOperation>().Select(o => o.Name);

            if (!dataProvider.TablesExist(dbContext.Database, tables))// alguna tabla fue eliminada
            {
                dbContext.DropTables(modelDiffer, sqlGenerator, true);

                //Crear nuevamente las tablas
                var sqlOperations = sqlGenerator.Generate(upOperations);

                using (var t = dbContext.Database.BeginTransaction())
                {
                    sqlOperations.ExecuteNonQuery(t.Connection);
                    t.Commit();
                }
            }
        }

        public static void DropTables(this DbContext dbContext,
                                           IMigrationsModelDiffer modelDiffer,
                                           IMigrationsSqlGenerator sqlGenerator,
                                           bool failSilently = false)
        {
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
                    catch (Exception e)
                    {
                        if (!failSilently)
                            throw e;
                    }
                }
                t.Commit();
            }
        }

        public static Type FindObjectContext(this System.Reflection.Assembly componentAssembly)
        {
            return componentAssembly.GetExportedTypes().FirstOrDefault(t => t.IsSubclassOf(typeof(DbContext)));
        }

    }
}
