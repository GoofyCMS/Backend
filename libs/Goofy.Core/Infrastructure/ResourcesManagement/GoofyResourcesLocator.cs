using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Goofy.Core.Infrastructure
{
    public class GoofyResourcesLocator : IResourcesLocator
    {
        private readonly IAssembliesProvider _assembliesProvider;

        public GoofyResourcesLocator(IAssembliesProvider resourcesLocator)
        {
            _assembliesProvider = resourcesLocator;
        }

        public IEnumerable<Type> FindClassesOfType(Type type)
        {
            var result = new List<Type>();
            foreach (var t in _assembliesProvider.GetAssemblies().SelectMany(assembly => assembly.GetExportedTypes()))
            {
                var typeInfo = t.GetTypeInfo();
                if (type.IsAssignableFrom(t) && typeInfo.IsClass)
                {
                    if (!typeInfo.IsInterface)
                    {
                        yield return t;
                    }
                }
            }
        }

        public IEnumerable<Type> FindClassesOfType<T>()
        {
            return FindClassesOfType(typeof(T));
        }
    }
}
