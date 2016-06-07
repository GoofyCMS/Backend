using System.Reflection;
using System.Collections.Generic;

namespace Goofy.Application.PluggableCore.Abstractions
{
    public interface IPluginManager
    {
        IPluginAssemblyProvider PluginAssemblyProvider { get; }
        IEnumerable<string> GetPlugins { get; }
        IEnumerable<Assembly> GetPluginAssembly { get; }
        IEnumerable<Assembly> GetAssembliesPerLayer(AppLayer layer);
        IEnumerable<Assembly> GetAssembliesByPluginName(string pluginName);
        PluginEnabledDisabledResult Enable(int pluginId);
        PluginEnabledDisabledResult Disable(int pluginId);
    }
}
