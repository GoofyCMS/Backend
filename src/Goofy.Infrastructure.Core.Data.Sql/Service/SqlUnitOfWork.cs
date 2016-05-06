using Goofy.Infrastructure.Core.Data.Service;
using Microsoft.Data.Entity;

namespace Goofy.Infrastructure.Core.Data.Sql.Service
{
    public abstract class SqlUnitOfWork : UnitOfWork
    {
        public SqlUnitOfWork(string connectionString)
            : base(connectionString)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
