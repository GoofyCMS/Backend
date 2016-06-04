using System.Collections.Generic;
using System.Reflection;

namespace Goofy.Application.PluggableCore.Services
{
    public interface IPluginManager
    {
        IEnumerable<Assembly> GetPluginAssembly { get; }
        IEnumerable<Assembly> GetAssembliesPerLayer(AppLayer layer);
        IEnumerable<Assembly> GetAssembliesByPluginName(string pluginName);
    }
}
