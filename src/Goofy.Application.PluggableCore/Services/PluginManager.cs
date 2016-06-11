using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Goofy.Application.PluggableCore.Abstractions;
using Goofy.Domain.PluggableCore.Service.Data;
using Goofy.Domain.PluggableCore.Entity;
using Goofy.Domain.Core.Service.Data;
using Goofy.Domain.PluggableCore.Extensions;
using Goofy.Application.PluggableCore.Extensions;

namespace Goofy.Application.PluggableCore.Services
{
    public class PluginManager : IPluginManager
    {
        //private IDictionary<string, IEnumerable<Assembly>> _plugins;
        private readonly IRepository<Plugin> _pluginRepository;
        private readonly IServiceProvider _services;

        public PluginManager(IServiceProvider services, IPluginAssemblyProvider pluginAssemblyProvider, IPluginUnitOfWork pluginContext)
        {

            PluginAssemblyProvider = pluginAssemblyProvider;
            PluginContext = pluginContext;
            _pluginRepository = PluginContext.Set<Plugin>();
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

        public IPluginUnitOfWork PluginContext { get; private set; }

        public IEnumerable<Assembly> GetAssembliesPerLayer(AppLayer layer)
        {
            return PluginAssemblyProvider.PluginAssemblies.Values.SelectMany(assemblies => assemblies.GetAssembliesPerLayer(layer));
        }

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
            PluginContext.SaveChanges();

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
            PluginContext.SaveChanges();

            return PluginEnabledDisabledResult.Ok;
        }
    }
}
