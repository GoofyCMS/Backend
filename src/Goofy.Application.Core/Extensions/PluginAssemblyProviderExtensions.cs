
using Goofy.Application.Core.Abstractions;
using System.Linq;
using System.Reflection;

namespace Goofy.Application.Core.Extensions
{
    public static class PluginAssemblyProviderExtensions
    {
        public static string GetPluginContainingAssembly(this IPluginAssemblyProvider assemblyProvider, Assembly assembly)
        {
            return assemblyProvider.PluginAssemblies.Where(kvp => kvp.Value.Contains(assembly))
                .Select(kvp => kvp.Key)
                .FirstOrDefault();
        }
    }
}
