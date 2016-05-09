﻿using System.Data.Entity;
using Goofy.Domain.Plugins.Entity;
using Goofy.Infrastructure.Core.Data.Service;
using Goofy.Infrastructure.Plugins.Data.Configuration;

namespace Goofy.Infrastructure.Plugins.Data
{
    public class PluginsContext : UnitOfWork
    {
        public PluginsContext(string connection)
            : base(connection)
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