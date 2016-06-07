using System.Reflection;
using System.Collections.Generic;
using Goofy.Domain.Core.Service.Data;
using Goofy.Domain.PluggableCore.Entity;

namespace Goofy.Application.PluggableCore.Abstractions
{
    public interface IPluginManager
    {
        IPluginAssemblyProvider PluginAssemblyProvider { get; }
        IRepository<Plugin> Plugins { get;}
        IEnumerable<Assembly> GetAssembliesPerLayer(AppLayer layer);
        IEnumerable<Assembly> GetAssembliesByPluginName(string pluginName);
        PluginEnabledDisabledResult Enable(int pluginId);
        PluginEnabledDisabledResult Disable(int pluginId);
    }
}
