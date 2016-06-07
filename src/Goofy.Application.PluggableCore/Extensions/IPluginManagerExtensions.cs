using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Goofy.Application.PluggableCore.Abstractions;
using Goofy.Domain.PluggableCore.Entity;
using Goofy.Domain.PluggableCore.Extensions;

namespace Goofy.Application.PluggableCore.Extensions
{
    public static class IPluginManagerExtensions
    {
        public static IEnumerable<Assembly> GetInfrastructureAdapterAssemblies(this IPluginManager pluginManager)
        {
            return pluginManager.GetAssembliesPerLayer(AppLayer.Infrastructure).Where(ass => ass.GetName().Name.Contains("Adapter"));
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
