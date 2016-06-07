using Goofy.Application.PluggableCore.Abstractions;
using System.Collections.Generic;
using System.Reflection;

namespace Goofy.Application.PluggableCore
{
    public interface IPluginAssemblyProvider : IAssemblyProvider_
    {
        string PluginsDirectoryPath { get; }

        IDictionary<string, IEnumerable<Assembly>> PluginAssemblies { get; }
    }
}
