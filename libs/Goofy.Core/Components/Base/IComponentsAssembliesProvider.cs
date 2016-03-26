using System.Collections.Generic;
using System.Reflection;

namespace Goofy.Core.Components.Base
{
    public interface IComponentsAssembliesProvider
    {
        IEnumerable<Assembly> ComponentsAssemblies { get; }

        void LoadAssemblies();
    }
}
