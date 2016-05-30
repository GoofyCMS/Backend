using System.Reflection;
using System.Collections.Generic;

namespace Goofy.Application.Plugins.Abstractions
{
    public interface IAssemblyProvider_
    {
        IEnumerable<Assembly> GetAssemblies { get; }
    }
}
