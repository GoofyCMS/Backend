using Microsoft.Extensions.DependencyInjection;

namespace Goofy.Application.PluggableCore.Abstractions
{
    public abstract class PluginDependenciesAdder : IDesignTimeService
    {
        public PluginDependenciesAdder() { }
        public abstract void AddPluginExtraDependencies(IServiceCollection services, IPluginManager manager);
    }
}
