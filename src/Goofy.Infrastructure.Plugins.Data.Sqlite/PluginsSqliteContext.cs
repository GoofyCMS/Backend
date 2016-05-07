using Microsoft.Data.Entity;
using Goofy.Domain.Plugins.Entity;
using Goofy.Infrastructure.Core.Data.Sqlite;
using Goofy.Infrastructure.Plugins.Data.Configuration;

namespace Goofy.Infrastructure.Plugins.Data.Sqlite
{
    public class PluginsSqliteContext : SqliteUnitOfWork
    {
        public PluginsSqliteContext(string connectionString)
            : base(connectionString)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plugin>(etb => new PluginConfiguration().ConfigureEntity(etb));
        }
    }
}
