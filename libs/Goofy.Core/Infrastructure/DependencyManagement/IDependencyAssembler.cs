using Microsoft.Extensions.DependencyInjection;

namespace Goofy.Core.Infrastructure
{
    public interface IDependencyAssembler : ISortableTask
    {
        void Register(IServiceCollection services, IResourcesLoader loader);
    }
}
