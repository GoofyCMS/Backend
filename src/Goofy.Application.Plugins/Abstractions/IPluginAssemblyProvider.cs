using Goofy.Application.Plugins.Abstractions;

namespace Goofy.Application.Plugins
{
    public interface IPluginAssemblyProvider : IAssemblyProvider_
    {
        string PluginsDirectoryPath { get; }
    }
}
