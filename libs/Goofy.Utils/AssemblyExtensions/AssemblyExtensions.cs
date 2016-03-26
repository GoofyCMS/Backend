using System;
using System.Linq;
using System.Reflection;

namespace Goofy
{
    public static class AssemblyExtensions
    {
        public static Type FindExportedObject<T>(this Assembly assembly)
        {
            //return assembly.GetExportedTypes().FirstOrDefault(t => t.IsAssignableFrom(typeof(T)));
            return assembly.GetExportedTypes().FirstOrDefault(t => typeof(T).IsAssignableFrom(t));

        }
    }
}
