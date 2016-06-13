using System.Reflection;
using System.Collections.Generic;

namespace Goofy.Application.Core.Abstractions
{
    public interface IAssemblyProvider_
    {
        IEnumerable<Assembly> Assemblies { get; }
    }
}
