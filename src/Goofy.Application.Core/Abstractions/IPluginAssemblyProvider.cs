using System.Reflection;
using System.Collections.Generic;

namespace Goofy.Application.Core.Abstractions
{
    public interface IPluginAssemblyProvider : IAssemblyProvider_
    {
        string PluginsDirectoryPath { get; }

        IDictionary<string, IEnumerable<Assembly>> PluginAssemblies { get; }
    }
}
