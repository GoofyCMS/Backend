using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Goofy.Domain.Plugins;

namespace Goofy.Application.Plugins.Services
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
            foreach (var pluginFolder in Directory.EnumerateDirectories(_pluginAssemblyProvider.PluginsDirectoryPath))
            {
                var dlls = Directory.EnumerateFiles(pluginFolder, "*.dll", SearchOption.TopDirectoryOnly)
                                    .Select(Path.GetFileNameWithoutExtension);

                _plugins.Add(Path.GetDirectoryName(pluginFolder),
                            _pluginAssemblyProvider.GetAssemblies.Where(ass => dlls.Contains(ass.GetName().Name)).ToArray());
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

        IEnumerable<Assembly> GetAssembliesPerLayer(AppLayer layer)
        {
            return _plugins.Values.SelectMany(assemblies => assemblies.Where(ass => Regex.IsMatch(ass.GetName().Name, GetPattern(layer))));
        }

        public void Install(int pluginId) { }
        public void Uninstall(int pluginId) { }
    }
}
