using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Goofy.Application.PluggableCore.Extensions
{
    public static class AssemblyEnumerableUtils
    {
        public static IEnumerable<Type> FindClassesOfType(this IEnumerable<Assembly> assemblies, Type type)
        {
            var wantedTypeInfo = type.GetTypeInfo();
            var result = new List<Type>();
            foreach (var t in assemblies.SelectMany(assembly => assembly.GetExportedTypes()))
            {
                if (t.IsClassOfType(type))
                {
                    yield return t;
                }
                //var currentTypeInfo = t.GetTypeInfo();
                //if (!currentTypeInfo.IsInterface && !currentTypeInfo.IsAbstract && currentTypeInfo.IsClass)
                //{
                //    if (type.IsAssignableFrom(t))
                //    {
                //        yield return t;
                //    }
                //else if (wantedTypeInfo.IsGenericTypeDefinition)
                //{
                //    //currentTypeInfo.GetGenericParameterConstraints
                //    //var genericArguments = currentTypeInfo.GetGenericArguments();
                //    if (currentTypeInfo.IsGenericParameter)
                //    {
                //        var newWantedTypeInfo = wantedTypeInfo.MakeGenericType(currentTypeInfo.GetGenericParameterConstraints()).GetTypeInfo();
                //        if (newWantedTypeInfo.IsAssignableFrom(currentTypeInfo))
                //            yield return t;
                //    }
                //}
                //}
                //else if (wantedTypeInfo.IsGenericType && currentTypeInfo.IsGenericTypeDefinition)
                //{
                //    var newType = type.MakeGenericType(currentTypeInfo.GetGenericArguments());
                //    return
                //    }
            }
        }

        public static bool IsClassOfType(this Type findedType, Type serviceType)
        {
            var currentTypeInfo = findedType.GetTypeInfo();
            if (!currentTypeInfo.IsInterface && !currentTypeInfo.IsAbstract && currentTypeInfo.IsClass)
            {
                if (serviceType.IsAssignableFrom(findedType))
                {
                    return true;
                }
            }
            return false;
        }

        public static IEnumerable<Type> FindClassesOfType<T>(this IEnumerable<Assembly> assemblies)
        {
            return assemblies.FindClassesOfType(typeof(T));
        }
    }
}
