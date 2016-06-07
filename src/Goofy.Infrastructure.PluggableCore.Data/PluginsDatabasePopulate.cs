using System;
using System.Linq;
using Goofy.Application.PluggableCore.Abstractions;
using Goofy.Domain.PluggableCore.Entity;
using Goofy.Domain.PluggableCore.Service.Data;

namespace Goofy.Infrastructure.PluggableCore.Data
{
    public class PluginsDatabasePopulate : IRunAtStartup
    {
        private readonly IPluginManager _pluginManager;
        private readonly IPluginUnitOfWork _pluginsContext;

        public PluginsDatabasePopulate(IPluginManager pluginManager, IPluginUnitOfWork pluginsContext)
        {
            _pluginManager = pluginManager;
            _pluginsContext = pluginsContext;
        }

        public int Order
        {
            get
            {
                return -1000;
            }
        }

        public void Run()
        {
            var pluginsRepository = _pluginsContext.Set<Plugin>();
            var currentPlugins = pluginsRepository.GetAll().ToArray();
            foreach (var loadedPlugin in _pluginManager.GetPlugins)
            {
                if (!currentPlugins.Where(p => p.Name == loadedPlugin).Any())
                {
                    pluginsRepository.Add(new Plugin { Name = loadedPlugin, Enabled = false });
                }
            }
            _pluginsContext.SaveChanges();
        }
    }
}
