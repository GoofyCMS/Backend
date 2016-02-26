using System;

namespace Goofy.Core.Infrastructure
{
    public interface IDependencyContainer : IScope
    {
        IScope Scope();

        void Register<T>() where T : class;
        object Resolve(Type serviceType, IScope scope);
        void RegisterInstanceAsSingleton<T>(T instance) where T : class;
        void RegisterDependency<TService, TDependency>() where TService : TDependency;
        void RegisterDependency(Type serviceType, Type dependencyType);
        void RegisterDependencyAsSingleton<TService, TDependency>() where TService : TDependency;
    }
}
