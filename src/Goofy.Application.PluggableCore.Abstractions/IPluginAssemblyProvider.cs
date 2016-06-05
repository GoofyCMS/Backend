using Goofy.Application.PluggableCore.Abstractions;

namespace Goofy.Application.PluggableCore
{
    public interface IPluginAssemblyProvider : IAssemblyProvider_
    {
        string PluginsDirectoryPath { get; }
    }
}
