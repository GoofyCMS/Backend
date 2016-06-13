using Goofy.Application.Administration.Services.Abstractions;
using Goofy.Application.Core.Extensions;
using Goofy.Domain.Administration.Entity;
using Goofy.Domain.Administration.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Goofy.Application.Administration.Extensions
{
    public static class PluginManagerExtensions
    {
        public static IEnumerable<Assembly> GetInfrastructureAdapterAssemblies(this IPluginManager pluginManager)
        {
            return pluginManager.PluginAssemblyProvider.Assemblies.GetInfrastructureAdapterAssemblies();
        }

        public static Plugin GetPluginContainigAssembly(this IPluginManager pluginManager, Assembly assembly)
        {
            var pluginName = pluginManager.PluginAssemblyProvider.PluginAssemblies.Where(kvp => kvp.Value.Contains(assembly))
                                                                                  .Select(kvp => kvp.Key)
                                                                                  .FirstOrDefault();
            if (pluginName == null)
            {
                return null;
            }

            return pluginManager.Plugins.GetPluginByName(pluginName);
        }
    }
}
