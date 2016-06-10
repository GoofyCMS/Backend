using Microsoft.Extensions.DependencyInjection;

namespace Goofy.Application.PluggableCore.Abstractions
{
    public interface IDependencyRegistrar
    {
        void ConfigureServices(IServiceCollection services);
    }
}
