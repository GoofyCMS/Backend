using System.Collections.Generic;
using Microsoft.Data.Entity.Infrastructure;

namespace Goofy.Data.DataProvider
{
    public interface IEntityFrameworkDataProvider
    {
        //Consultas del proveedor de datos
        bool TablesExist(DatabaseFacade database, IEnumerable<string> tables);
    }
}
