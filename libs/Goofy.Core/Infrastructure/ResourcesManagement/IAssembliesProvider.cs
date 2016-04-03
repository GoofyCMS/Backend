using System.Collections.Generic;
using System.Reflection;

namespace Goofy.Core.Infrastructure
{
    public interface IAssembliesProvider
    {
        IEnumerable<Assembly> GetAssemblies();
    }
}
    