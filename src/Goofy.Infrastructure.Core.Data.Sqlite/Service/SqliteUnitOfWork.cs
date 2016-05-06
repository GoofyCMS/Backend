using Microsoft.Data.Entity;
using Goofy.Infrastructure.Core.Data.Service;

namespace Goofy.Infrastructure.Core.Data.Sqlite.Service
{
    public abstract class SqliteUnitOfWork : UnitOfWork
    {
        public SqliteUnitOfWork(string connectionString)
            : base(connectionString)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }
    }
}
