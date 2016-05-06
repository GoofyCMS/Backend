using System.Reflection;
using System.Collections.Generic;

namespace Goofy.Domain.Core.Abstractions
{
    public interface IAssemblyProvider_
    {
        IEnumerable<Assembly> GetAssemblies { get; }
    }
}
