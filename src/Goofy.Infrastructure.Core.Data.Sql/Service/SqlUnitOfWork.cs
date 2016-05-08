using Goofy.Infrastructure.Core.Data.Service;

namespace Goofy.Infrastructure.Core.Data.Sql.Service
{
    public abstract class SqlUnitOfWork : UnitOfWork
    {
        public SqlUnitOfWork(string connectionString)
            : base(connectionString)
        {
        }
    }
}
