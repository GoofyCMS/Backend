using System;
using Microsoft.Extensions.DependencyInjection;

namespace Goofy.Core.DependencyInjection.DesignTimeExtensions
{
    public static class DesignTimeExtensions
    {
        static IServiceProvider _serviceProvider;
        static int servicesNumber;

        public static object Resolve(this IServiceCollection services, Type serviceType)
        {
            if (_serviceProvider == null || servicesNumber != services.Count)
            {
                _serviceProvider = services.BuildServiceProvider();
                servicesNumber = services.Count;
            }
            return ActivatorUtilities.GetServiceOrCreateInstance(_serviceProvider, serviceType);
        }

        public static T Resolve<T>(this IServiceCollection services)
        {
            return (T)services.Resolve(typeof(T));
        }
    }
}
