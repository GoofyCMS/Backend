using Microsoft.Extensions.DependencyInjection;

namespace Goofy.Core.Infrastructure
{
    public interface IEngine
    {
        void Start(IServiceCollection services);
    }
}
