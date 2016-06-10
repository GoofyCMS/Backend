using Goofy.Application.PluggableCore.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Goofy.Application.PluggableCore.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Assembly> GetAssembliesPerLayer(this IEnumerable<Assembly> assemblies, AppLayer layer)
        {
            return assemblies.Where(ass => Regex.IsMatch(ass.GetName().Name, GetPattern(layer)));
        }

        public static IEnumerable<Assembly> GetInfrastructureAdapterAssemblies(this IEnumerable<Assembly> assemblies)
        {
            return assemblies.GetAssembliesPerLayer(AppLayer.Infrastructure).Where(ass => ass.GetName().Name.Contains("Adapter"));
        }

        static string GetPattern(AppLayer layer)
        {
            switch (layer)
            {
                case AppLayer.Domain:
                    return "Goofy.Domain.*";
                case AppLayer.Infrastructure:
                    return "Goofy.Infrastructure.*";
                case AppLayer.Application:
                    return "Goofy.Application.*";
                default: return "Goofy.Presentation.*";
            }
        }
    }
}
