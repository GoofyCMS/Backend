using System.Collections.Generic;
using System.Reflection;

namespace Goofy.Application.PluggableCore.Abstractions
{
    public interface IPluginManager
    {
        IPluginAssemblyProvider PluginAssemblyProvider { get; }
        IEnumerable<Assembly> GetPluginAssembly { get; }
        IEnumerable<Assembly> GetAssembliesPerLayer(AppLayer layer);
        IEnumerable<Assembly> GetAssembliesByPluginName(string pluginName);
    }
}
