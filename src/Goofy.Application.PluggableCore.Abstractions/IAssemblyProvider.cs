using System.Reflection;
using System.Collections.Generic;

namespace Goofy.Application.PluggableCore.Abstractions
{
    public interface IAssemblyProvider_
    {
        IEnumerable<Assembly> Assemblies { get; }
    }
}
