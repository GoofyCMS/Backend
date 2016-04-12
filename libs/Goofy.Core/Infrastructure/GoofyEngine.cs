using System;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;
using Goofy.Core.Configuration;
using Goofy.Extensions;
using System.Reflection;
using System.Collections.Generic;

namespace Goofy.Core.Infrastructure
{
    public class GoofyEngine : IEngine
    {
        protected GoofyCoreConfiguration _goofyCoreConfiguration;
        protected readonly IServiceCollection _services;
        protected readonly IResourcesLocator _resourcesLoader;

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


        public GoofyEngine(IServiceCollection services, IResourcesLocator resourcesLoader)
        {
            _services = services;
            _resourcesLoader = resourcesLoader;
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
            }
        }

        private bool ServiceIsDesignTimeService(ServiceDescriptor serviceDescriptor)
        {
            var implementationType = serviceDescriptor.ImplementationType;
            if (implementationType != null)
                return typeof(IDesignTimeService).GetTypeInfo().IsAssignableFrom(implementationType.GetTypeInfo());

            var implementationInstance = serviceDescriptor.ImplementationInstance;
            if (implementationInstance != null)
            {
                return typeof(IDesignTimeService).GetTypeInfo().IsAssignableFrom(implementationInstance.GetType().GetTypeInfo());
            }
            return false;
        }
    }
}
