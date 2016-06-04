using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Goofy.Application.PluggableCore.Services
{
    public class PluginManager : IPluginManager
    {
        private Dictionary<string, IEnumerable<Assembly>> _plugins;

        private readonly IPluginAssemblyProvider _pluginAssemblyProvider;

        public PluginManager(IPluginAssemblyProvider pluginAssemblyProvider)
        {
            _pluginAssemblyProvider = pluginAssemblyProvider;
            StartPluginsConfiguration();
        }

        void StartPluginsConfiguration()
        {
            _plugins = new Dictionary<string, IEnumerable<Assembly>>();

            if (Directory.Exists(_pluginAssemblyProvider.PluginsDirectoryPath))
            {
                foreach (var pluginFolder in Directory.EnumerateDirectories(_pluginAssemblyProvider.PluginsDirectoryPath))
                {
                    var dlls = Directory.EnumerateFiles(pluginFolder, "*.dll", SearchOption.TopDirectoryOnly)
                                        .Select(Path.GetFileNameWithoutExtension);

                    _plugins.Add(Path.GetDirectoryName(pluginFolder),
                                _pluginAssemblyProvider.GetAssemblies.Where(ass => dlls.Contains(ass.GetName().Name)).ToArray());
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
                default: return "Goofy.Web.*";
            }
        }

        public IEnumerable<Assembly> GetPluginAssembly
        {
            get
            {
                return _pluginAssemblyProvider.GetAssemblies;
            }
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
