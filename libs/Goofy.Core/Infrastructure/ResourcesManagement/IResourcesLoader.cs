using System;
using System.Collections.Generic;

namespace Goofy.Core.Infrastructure
{
    public interface IResourcesLoader
    {
        IResourcesLocator Locator { get; }
        IEnumerable<Type> FindClassesOfType(Type type, bool onlyConcreteClasses = true);
        IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClases = true);
    }
}
