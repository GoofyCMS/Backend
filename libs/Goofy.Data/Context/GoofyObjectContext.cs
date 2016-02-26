using Microsoft.Data.Entity;

namespace Goofy.Data
{
    public abstract class GoofyObjectContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
            GoofyDataAccessManager.GoofyDataConfiguration.Provider.ConfigureDbContextProvider(options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

    }
}
