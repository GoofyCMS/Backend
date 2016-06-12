using System.Reflection;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Goofy.Application.PluggableCore.Abstractions
{
    public interface IDependencyRegistrar
    {
        void ConfigureServices(IServiceCollection services, IEnumerable<Assembly> assemblies);
    }
}
