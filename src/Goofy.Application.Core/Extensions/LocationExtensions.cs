using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Goofy.Application.Core.Extensions
{
    public static class LocationExtensions
    {
        /*
            TODO: Hacer tests para esto, 
        */
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
