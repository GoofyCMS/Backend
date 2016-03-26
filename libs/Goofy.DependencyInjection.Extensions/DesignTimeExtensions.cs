using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
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

        public static IServiceCollection Remove<TService>(this IServiceCollection services, bool failSilently=false)
        {
            var componentDbContextPopulatorDescriptor = services.Where(sd => sd.ServiceType == typeof(TService)).FirstOrDefault();
            if (componentDbContextPopulatorDescriptor != null)
                services.Remove(componentDbContextPopulatorDescriptor);
            else if (componentDbContextPopulatorDescriptor == null && !failSilently)
                throw new InvalidOperationException();

            return services;
        }
    }
}
