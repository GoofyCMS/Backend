
using Goofy.Application.Core.Abstractions;
using Goofy.Domain.Administration.Entity;
using Goofy.Domain.Core.Service.Data;
using System.Collections.Generic;
using System.Reflection;

namespace Goofy.Application.Administration.Services.Abstractions
{
    public interface IPluginManager
    {
        IPluginAssemblyProvider PluginAssemblyProvider { get; }
        IRepository<Plugin> Plugins { get; }
        IEnumerable<Assembly> GetAssembliesByPluginName(string pluginName);
        PluginEnabledDisabledResult Enable(int pluginId);
        PluginEnabledDisabledResult Disable(int pluginId);
    }
}
