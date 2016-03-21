using System.Collections.Generic;
using Microsoft.Data.Entity.Infrastructure;

namespace Goofy.Data.DataProvider
{
    public abstract class DataProviderWrapperBase : IEntityFrameworkDataProvider
    {
        public DataProviderWrapperBase()
        {
        }
        //Operaciones en la base de datos
        public abstract bool TablesExist(DatabaseFacade database, IEnumerable<string> tables);
    }
}
