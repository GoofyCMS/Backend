using System.Reflection;

namespace Goofy.Core.Infrastructure
{
    public interface IResourcesLocator
    {
        Assembly[] GetAssemblies();
    }
}
