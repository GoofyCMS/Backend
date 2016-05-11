using System;
using Microsoft.Extensions.DependencyInjection;
using Goofy.Domain.Core.Abstractions;

namespace Goofy.Application.Core.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static void StartEngine(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var engine = serviceProvider.GetRequiredService<IEngine>();
            engine.Start();
        }
    }
}
