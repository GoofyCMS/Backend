using Goofy.Application.Administration.Services.Abstractions;
using Goofy.Application.Core.Abstractions;
using Goofy.Domain.Administration.Entity;
using Goofy.Domain.Administration.Service.Data;
using System.Linq;

namespace Goofy.Application.Administration
{
    public class PluginsDatabasePopulate : IRunAtStartup
    {
        private readonly IPluginManager _pluginManager;
        private readonly IAdministrationUnitOfWork _adminContext;

        public PluginsDatabasePopulate(IPluginManager pluginManager, IAdministrationUnitOfWork adminContext)
        {
            _pluginManager = pluginManager;
            _adminContext = adminContext;
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
            var pluginsRepository = _adminContext.Set<Plugin>();
            var currentPlugins = pluginsRepository.GetAll().ToArray();
            foreach (var loadedPlugin in _pluginManager.PluginAssemblyProvider.PluginAssemblies.Keys)
            {
                if (!currentPlugins.Where(p => p.Name == loadedPlugin).Any())
                {
                    pluginsRepository.Add(new Plugin { Name = loadedPlugin, Enabled = false });
                }
            }
            _adminContext.SaveChanges();
        }
    }
}
