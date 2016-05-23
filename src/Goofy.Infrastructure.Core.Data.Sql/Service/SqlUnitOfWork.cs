using Goofy.Infrastructure.Core.Data.Service;
using System;

namespace Goofy.Infrastructure.Core.Data.Sql.Service
{
    public abstract class SqlUnitOfWork : UnitOfWork
    {
        public SqlUnitOfWork(IServiceProvider services)
            : base(services)
        {
        }
    }
}
