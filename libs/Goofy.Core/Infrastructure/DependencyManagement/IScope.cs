using System;

namespace Goofy.Core.Infrastructure
{
    public interface IScope
    {
        T Resolve<T>();
        object Resolve(Type serviceType);
        bool IsRegistered(Type serviceType);
    }
}
