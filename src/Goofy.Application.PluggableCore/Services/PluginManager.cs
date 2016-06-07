using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Goofy.Application.PluggableCore.Abstractions;
using Goofy.Domain.PluggableCore.Service.Data;
using Goofy.Domain.PluggableCore.Entity;
using Goofy.Domain.Core.Service.Data;
using Goofy.Domain.Core;
using Goofy.Infrastructure.Core.Data.Utils;

namespace Goofy.Application.PluggableCore.Services
{
    public class PluginManager : IPluginManager
    {
        private Dictionary<string, IEnumerable<Assembly>> _plugins;
        private readonly IRepository<Plugin> _pluginRepository;
        private readonly IServiceProvider _services;

        public PluginManager(IServiceProvider services, IPluginAssemblyProvider pluginAssemblyProvider, IPluginUnitOfWork pluginContext)
        {

            PluginAssemblyProvider = pluginAssemblyProvider;
            StartPluginsConfiguration();
            PluginContext = pluginContext;
            _pluginRepository = PluginContext.Set<Plugin>();
            _services = services;
        }

        void StartPluginsConfiguration()
        {
            _plugins = new Dictionary<string, IEnumerable<Assembly>>();

            if (Directory.Exists(PluginAssemblyProvider.PluginsDirectoryPath))
            {
                foreach (var pluginFolder in Directory.EnumerateDirectories(PluginAssemblyProvider.PluginsDirectoryPath))
                {
                    var dlls = Directory.EnumerateFiles(pluginFolder, "*.dll", SearchOption.TopDirectoryOnly)
                                        .Select(Path.GetFileNameWithoutExtension);

                    _plugins.Add(new DirectoryInfo(pluginFolder).Name,
                                PluginAssemblyProvider.GetAssemblies.Where(ass => dlls.Contains(ass.GetName().Name)).ToArray());
                }
            }
        }

        string GetPattern(AppLayer layer)
        {
            switch (layer)
            {
                case AppLayer.Domain:
                    return "Goofy.Domain.*";
                case AppLayer.Infrastructure:
                    return "Goofy.Infrastructure.*";
                case AppLayer.Application:
                    return "Goofy.Application.*";
                default: return "Goofy.Presentation.*";
            }
        }

        public IEnumerable<Assembly> GetPluginAssembly
        {
            get
            {
                return PluginAssemblyProvider.GetAssemblies;
            }
        }

        public IPluginAssemblyProvider PluginAssemblyProvider
        {
            get;
            private set;
        }

        public IEnumerable<string> GetPlugins
        {
            get
            {
                return _plugins.Keys;
            }
        }

        public IPluginUnitOfWork PluginContext { get; private set; }

        public IEnumerable<Assembly> GetAssembliesPerLayer(AppLayer layer)
        {
            return _plugins.Values.SelectMany(assemblies => assemblies.Where(ass => Regex.IsMatch(ass.GetName().Name, GetPattern(layer))));
        }

        public IEnumerable<Assembly> GetAssembliesByPluginName(string pluginName)
        {
            try
            {
                return _plugins[pluginName];
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException(string.Format($"Plugin \"{pluginName}\" doesn't exist."));
            }
        }

        public PluginEnabledDisabledResult Enable(int pluginId)
        {
            var plugin = GetPlugin(pluginId);
            if (plugin == null)
                return PluginEnabledDisabledResult.NotFound;
            if (plugin.Enabled)
                return PluginEnabledDisabledResult.AlreadyEnabled;
            plugin.Enabled = true;
            _pluginRepository.Modify(plugin);
            PluginContext.SaveChanges();

            var unitOfWork = GetUnitOfWork(plugin.Name);
            unitOfWork?.CreateTablesIfNotExist();
            return PluginEnabledDisabledResult.Ok;
        }

        public PluginEnabledDisabledResult Disable(int pluginId)
        {
            var plugin = GetPlugin(pluginId);
            if (plugin == null)
                return PluginEnabledDisabledResult.NotFound;
            if (!plugin.Enabled)
                return PluginEnabledDisabledResult.AlreadyDisabled;
            plugin.Enabled = false;
            _pluginRepository.Modify(plugin);
            PluginContext.SaveChanges();

            var unitOfWork = GetUnitOfWork(plugin.Name);
            unitOfWork?.DropTables();
            return PluginEnabledDisabledResult.Ok;
        }

        private Plugin GetPlugin(int pluginId)
        {
            return _pluginRepository.Find(pluginId);
        }

        private IUnitOfWork GetUnitOfWork(string pluginName)
        {
            var assemblies = GetAssembliesPerLayer(AppLayer.Infrastructure).Where(ass => ass.GetName().Name.Contains(pluginName));
            var unitOfWorkType = assemblies.FindClassesOfType<IUnitOfWork>().FirstOrDefault();
            if (unitOfWorkType != null)
                return (IUnitOfWork)_services.GetService(unitOfWorkType);
            return null;
        }
    }
}
