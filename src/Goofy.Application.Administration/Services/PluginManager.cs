using Goofy.Application.Administration.Services.Abstractions;
using Goofy.Application.Core.Abstractions;
using Goofy.Domain.Administration.Entity;
using Goofy.Domain.Administration.Extensions;
using Goofy.Domain.Administration.Service.Data;
using Goofy.Domain.Core.Service.Data;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Goofy.Application.Administration.Services
{
    public class PluginManager : IPluginManager
    {
        //private IDictionary<string, IEnumerable<Assembly>> _plugins;
        private readonly IRepository<Plugin> _pluginRepository;
        private readonly IServiceProvider _services;

        public PluginManager(IServiceProvider services, IPluginAssemblyProvider pluginAssemblyProvider, IAdministrationUnitOfWork adminContext)
        {

            PluginAssemblyProvider = pluginAssemblyProvider;
            AdminContext = adminContext;
            _pluginRepository = AdminContext.Set<Plugin>();
            _services = services;
        }

        public IPluginAssemblyProvider PluginAssemblyProvider
        {
            get;
            private set;
        }

        public IRepository<Plugin> Plugins
        {
            get
            {
                return _pluginRepository;
            }
        }

        public IAdministrationUnitOfWork AdminContext { get; private set; }


        public IEnumerable<Assembly> GetAssembliesByPluginName(string pluginName)
        {
            try
            {
                return PluginAssemblyProvider.PluginAssemblies[pluginName];
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException(string.Format($"Plugin \"{pluginName}\" doesn't exist."));
            }
        }

        public PluginEnabledDisabledResult Enable(int pluginId)
        {
            var plugin = Plugins.GetPluginById(pluginId);
            if (plugin == null)
                return PluginEnabledDisabledResult.NotFound;
            if (plugin.Enabled)
                return PluginEnabledDisabledResult.AlreadyEnabled;
            plugin.Enabled = true;
            _pluginRepository.Modify(plugin);
            AdminContext.SaveChanges();

            return PluginEnabledDisabledResult.Ok;
        }

        public PluginEnabledDisabledResult Disable(int pluginId)
        {
            var plugin = Plugins.GetPluginById(pluginId);
            if (plugin == null)
                return PluginEnabledDisabledResult.NotFound;
            if (!plugin.Enabled)
                return PluginEnabledDisabledResult.AlreadyDisabled;
            plugin.Enabled = false;
            _pluginRepository.Modify(plugin);
            AdminContext.SaveChanges();

            return PluginEnabledDisabledResult.Ok;
        }
    }
}
