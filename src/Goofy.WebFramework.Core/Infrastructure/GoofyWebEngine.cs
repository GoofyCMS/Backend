using Microsoft.Extensions.DependencyInjection;
using Goofy.Core.Infrastructure;

namespace Goofy.WebFramework.Core.Infrastructure
{
    public class GoofyWebEngine : GoofyEngine
    {
        public GoofyWebEngine(IResourcesLoader resourcesLoader, IResourcesLocator resourcesLocator) : base(resourcesLoader, resourcesLocator)
        {
        }

    }
}
