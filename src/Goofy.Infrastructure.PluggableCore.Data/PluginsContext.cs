using System;
using System.Data.Entity;
using Goofy.Domain.PluggableCore.Entity;
using Goofy.Infrastructure.Core.Data.Service;
using Goofy.Infrastructure.PluggableCore.Data.Configuration;
using Goofy.Domain.PluggableCore.Service.Data;

namespace Goofy.Infrastructure.PluggableCore.Data
{
    public class PluginsContext : UnitOfWork, IPluginUnitOfWork
    {
        public PluginsContext(string connectionString)
            : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
        }

        public virtual DbSet<Plugin> Plugins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PluginConfiguration());
        }
    }
}
