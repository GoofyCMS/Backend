using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Goofy.Application.PluggableCore.Abstractions;

namespace Goofy.Application.PluggableCore.Services
{
    public class PluginManager : IPluginManager
    {
        private Dictionary<string, IEnumerable<Assembly>> _plugins;

        public PluginManager(IPluginAssemblyProvider pluginAssemblyProvider)
        {
            PluginAssemblyProvider = pluginAssemblyProvider;
            StartPluginsConfiguration();
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

                    _plugins.Add(Path.GetDirectoryName(pluginFolder),
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

        public void Unable(int pluginId) { }
        public void Disable(int pluginId) { }
    }
}
