using Goofy.Domain.Core.Abstractions;

namespace Goofy.Domain.Plugins
{
    public interface IPluginAssemblyProvider : IAssemblyProvider_
    {
        string PluginsDirectoryPath { get; }
    }
}
