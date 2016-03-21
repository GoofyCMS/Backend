using Goofy.Data.DataProvider.Services;
using Microsoft.Data.Entity;

namespace Goofy.Data
{
    public abstract class GoofyObjectContext : DbContext
    {
        private readonly IDataProviderConfigurator _dataProviderConfigurator;

        public GoofyObjectContext(IDataProviderConfigurator dataProviderConfigurator)
        {
            _dataProviderConfigurator = dataProviderConfigurator;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
            _dataProviderConfigurator.ConfigureDbContextProvider(options);
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
