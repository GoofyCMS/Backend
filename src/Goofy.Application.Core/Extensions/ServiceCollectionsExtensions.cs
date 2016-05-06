using System;
using Microsoft.Extensions.DependencyInjection;
using Goofy.Domain.Core.Abstractions;

namespace Goofy.Application.Core.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceProvider ConfigureServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var engine = serviceProvider.GetRequiredService<IEngine>();
            return engine.Start();
        }
    }
}
