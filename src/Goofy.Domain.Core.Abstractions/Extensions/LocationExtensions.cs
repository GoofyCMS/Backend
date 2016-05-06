using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Goofy.Domain.Core.Abstractions
{
    public static class LocationExtensions
    {
        public static IEnumerable<Type> FindClassesOfType(this IEnumerable<Assembly> assemblies, Type type)
        {
            var result = new List<Type>();
            foreach (var t in assemblies.SelectMany(assembly => assembly.GetExportedTypes()))
            {
                var typeInfo = t.GetTypeInfo();
                if (type.IsAssignableFrom(t) && typeInfo.IsClass)
                {
                    if (!typeInfo.IsInterface && !typeInfo.IsAbstract)
                    {
                        yield return t;
                    }
                }
            }
        }

        public static IEnumerable<Type> FindClassesOfType<T>(this IEnumerable<Assembly> assemblies)
        {
            return assemblies.FindClassesOfType(typeof(T));
        }
    }
}
