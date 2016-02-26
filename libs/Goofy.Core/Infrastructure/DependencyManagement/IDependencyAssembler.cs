
namespace Goofy.Core.Infrastructure
{
    public interface IDependencyAssembler : ISortableTask
    {
        void Register(IDependencyContainer builder, IResourcesLoader loader);
    }
}
