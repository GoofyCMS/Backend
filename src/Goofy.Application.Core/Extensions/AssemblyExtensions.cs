using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Goofy.Application.Core.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Assembly> GetAssembliesPerLayer(this IEnumerable<Assembly> assemblies, params AppLayer[] layers)
        {
            return assemblies.Where(ass => MatchForAny(ass.GetName().Name, layers));
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

        static bool MatchForAny(string assemblyName, AppLayer[] layers)
        {
            return layers.Any(l => Regex.IsMatch(assemblyName, GetPattern(l)));
        }
    }
}
