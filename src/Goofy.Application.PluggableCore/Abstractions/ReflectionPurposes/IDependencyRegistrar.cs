using Microsoft.Extensions.DependencyInjection;

namespace Goofy.Application.PluggableCore.Abstractions
{
    public interface IDependencyRegistrar : ISortableTask
    {
        void ConfigureServices(IServiceCollection services);
    }
}
