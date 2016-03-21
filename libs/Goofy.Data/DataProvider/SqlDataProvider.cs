using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Infrastructure;

namespace Goofy.Data.DataProvider
{
    class SqlDataProviderWrapper : DataProviderWrapperBase
    {
        public override bool TablesExist(DatabaseFacade database, IEnumerable<string> tables)
        {
            throw new NotImplementedException();
        }
    }
}
