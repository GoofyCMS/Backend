using System.Data.Entity;
using Goofy.Domain.PluggableCore.Entity;
using Goofy.Infrastructure.Core.Data.Service;
using Goofy.Infrastructure.PluggableCore.Data.Configuration;
using System;
using Goofy.Domain.PluggableCore.Service.Data;

namespace Goofy.Infrastructure.PluggableCore.Data
{
    public class PluginsContext : UnitOfWork, IPluginUnitOfWork
    {
        public PluginsContext(IServiceProvider services)
            : base(services)
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
