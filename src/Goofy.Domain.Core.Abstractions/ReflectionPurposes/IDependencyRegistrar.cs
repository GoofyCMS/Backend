using Microsoft.Extensions.DependencyInjection;

namespace Goofy.Domain.Core.Abstractions
{
    public interface IDependencyRegistrar : ISortableTask
    {
        void ConfigureServices(IServiceCollection services);
    }
}
