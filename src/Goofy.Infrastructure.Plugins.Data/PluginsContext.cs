﻿using System.Data.Entity;
using Goofy.Domain.Plugins.Entity;
using Goofy.Infrastructure.Core.Data.Service;
using Goofy.Infrastructure.Plugins.Data.Configuration;
using System;
using Goofy.Domain.Plugins.Service.Data;

namespace Goofy.Infrastructure.Plugins.Data
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
