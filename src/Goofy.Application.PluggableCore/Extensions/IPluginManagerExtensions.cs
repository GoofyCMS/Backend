
using Goofy.Application.PluggableCore.Services;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Goofy.Application.PluggableCore.Extensions
{
    public static class IPluginManagerExtensions
    {
        public static IEnumerable<Assembly> GetInfrastructureAdapterAssemblies(this IPluginManager pluginManager)
        {
            return pluginManager.GetAssembliesPerLayer(AppLayer.Infrastructure).Where(ass => ass.GetName().Name.Contains("Adapter"));
        }
    }
}
