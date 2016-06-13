using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;

namespace Goofy.Application.Core.Abstractions
{
    public interface IDependencyRegistrar
    {
        void ConfigureServices(IServiceCollection services, IEnumerable<Assembly> assemblies);
    }
}
