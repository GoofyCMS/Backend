using Microsoft.Extensions.Configuration;
using Goofy.Core.Infrastructure;

namespace Goofy.WebFramework.Infrastructure
{
    public interface IGoofyDependencyAssembler: IDependencyAssembler
    {
        void RegisterWebDependencies(IWebDependencyContainer container, IResourcesLoader loader, IConfiguration config);
    }
}
