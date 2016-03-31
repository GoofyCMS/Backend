using System;
using System.Collections.Generic;

namespace Goofy.Core.Infrastructure
{
    public interface IResourcesLocator
    {
        IEnumerable<Type> FindClassesOfType(Type type);
        IEnumerable<Type> FindClassesOfType<T>();
    }
}
