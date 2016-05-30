using Microsoft.Extensions.DependencyInjection;

namespace Goofy.Application.Plugins.Abstractions
{
    public interface IDependencyRegistrar : ISortableTask
    {
        void ConfigureServices(IServiceCollection services);
    }
}
