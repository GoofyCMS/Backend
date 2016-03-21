using System.Collections.Generic;
using System.Linq;

using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Storage.Internal;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Storage;

namespace Goofy.Data.DataProvider
{
    public class SqliteDataProviderWrapper : DataProviderWrapperBase
    {
        private readonly ISqlCommandBuilder _commandBuilder;

        public SqliteDataProviderWrapper(ISqlCommandBuilder commandBuilder) : base()
        {
            _commandBuilder = commandBuilder;
        }

        public override bool TablesExist(DatabaseFacade database, IEnumerable<string> tables)
        {
            //return true;
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

            var command = _commandBuilder.Build(query, tablesArray);
            using (var t = database.BeginTransaction())
            {
                var count = (long)command.ExecuteScalar(t.Connection);
                return count == tablesArray.Length;
            }
        }

    }
}
