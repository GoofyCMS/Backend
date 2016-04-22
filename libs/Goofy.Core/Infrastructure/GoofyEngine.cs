using System;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Goofy.Core.Configuration;
using Goofy.Extensions;

namespace Goofy.Core.Infrastructure
{
    public class GoofyEngine : IEngine
    {
        protected GoofyCoreConfiguration _goofyCoreConfiguration;
        protected readonly IServiceCollection _services;
        protected readonly IResourcesLocator _resourcesLoader;
        private readonly ILogger<GoofyEngine> _logger;

        public GoofyCoreConfiguration GoofyCoreConfiguration
        {
            get
            {
                if (_goofyCoreConfiguration == null)
                {
                    _goofyCoreConfiguration = new GoofyCoreConfiguration();
                }
                return _goofyCoreConfiguration;
            }
        }


        public GoofyEngine(IServiceCollection services,
                           IResourcesLocator resourcesLoader,
                           ILogger<GoofyEngine> logger)
        {
            _services = services;
            _resourcesLoader = resourcesLoader;
            _logger = logger;
        }

        public virtual void Start()
        {
            /* Todas las tareas que se ejecutan a continuación se realizan luego de que los 
            ensamblados de los plugins son cargados */

            // Agregar las dependencias provistas por otros ensamblados(propios o de 3ros)
            RegisterDependencies();

            //Correr tareas de inicio provistas por otros ensamblados(propios o de 3ros)
            if (GoofyCoreConfiguration.RunStartupTasks)
            {
                ExecActionForeachSortableType<IRunAtStartup>(startupTask => startupTask.Run());
            }

            //Remover servicios que sólo son utilizados durante el inicio de Goofy
            RemoveDesignTimeServices();
            //Quitar IServiceCollection de las dependencias
            _services.Remove<IServiceCollection>();
        }


        private void ExecActionForeachSortableType<T>(Action<T> action) where T : ISortableTask
        {
            var depAssemblerTypes = _resourcesLoader.FindClassesOfType<T>()
                                                   .Select(t => (T)_services.Resolve(t));
            var assemblers = depAssemblerTypes.OrderBy(s => s.Order);
            foreach (var depAssembler in assemblers)
            {
                action(depAssembler);
            }
        }

        private void ExecActionForeachType<T>(Action<T> action)
        {
            var depAssemblerTypes = _resourcesLoader.FindClassesOfType<T>()
                                                   .Select(t => (T)_services.Resolve(t));
            foreach (var depAssembler in depAssemblerTypes)
            {
                action(depAssembler);
            }
        }

        protected virtual void RegisterDependencies()
        {
            ExecActionForeachSortableType<IDependencyAssembler>(
                                                                 d =>
                                                                 {
                                                                     d.Register(_services);
                                                                 }
                                                              );
        }

        protected virtual void RemoveDesignTimeServices()
        {
            var servicesForDeleting = _services.Where(s => ServiceIsDesignTimeService(s)).ToArray();
            foreach (var serviceDescriptor in servicesForDeleting)
            {
                _services.Remove(serviceDescriptor);
                var implementationType = serviceDescriptor.ImplementationType ?? serviceDescriptor.ImplementationInstance.GetType();
                _logger.LogInformation(string.Format("Remove implementationType \"{0}\", for service \"{1}\".", implementationType, serviceDescriptor.ServiceType));
            }
        }

        private bool ServiceIsDesignTimeService(ServiceDescriptor serviceDescriptor)
        {
            var serviceType = serviceDescriptor.ServiceType;
            if (serviceType != null)
                return typeof(IDesignTimeService).GetTypeInfo().IsAssignableFrom(serviceType.GetTypeInfo());
            return false;
        }
    }
}
