using System.Collections.Generic;
using System.Reflection;
using Goofy.Application.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Goofy.Application.ServerTime.Services;

namespace Goofy.Application.ServerTime.DependencyInjection
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void ConfigureServices(IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddScoped<Service>();
        }
    }
}
