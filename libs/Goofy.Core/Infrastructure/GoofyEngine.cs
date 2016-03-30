using System;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;
using Goofy.Core.Configuration;
using Goofy.Extensions;

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
            RegisterDependencies(_services);

            //Correr tareas de inicio provistas por otros ensamblados(propios o de 3ros)
            if (GoofyCoreConfiguration.RunStartupTasks)
            {
                var startupTasksTypes = _resourcesLoader.FindClassesOfType<IRunAtStartup>()
                                .Select(t => (IRunAtStartup)_services.Resolve(t)).ToArray();
                var startupTasks = startupTasksTypes.OrderBy(t => t.Order);
                foreach (var s in startupTasks)
                {
                    s.Run();
                }
            }

            //Quitar IServiceCollection de las dependencias
            _services.Remove<IServiceCollection>();
        }


        public void RegisterSortableDependencies<T>(Action<T> action) where T : ISortableTask
        {
            var depAssemblerTypes = _resourcesLoader.FindClassesOfType<T>()
                                                   .Select(t => (T)_services.Resolve(t));
            var assemblers = depAssemblerTypes.OrderBy(s => s.Order);
            foreach (var depAssembler in assemblers)
            {
                action(depAssembler);
            }
        }

        public void RegisterDependencies<T>(Action<T> action)
        {
            var depAssemblerTypes = _resourcesLoader.FindClassesOfType<T>()
                                                   .Select(t => (T)_services.Resolve(t));
            foreach (var depAssembler in depAssemblerTypes)
            {
                action(depAssembler);
            }
        }

        public virtual void RegisterDependencies(IServiceCollection services)
        {
            RegisterSortableDependencies<IDependencyAssembler>(
                                                                 d =>
                                                                 {
                                                                     d.Register(services);
                                                                 }
                                                              );
        }

    }
}
