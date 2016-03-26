using System;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;
using Goofy.Core.Configuration;

namespace Goofy.Core.Infrastructure
{
    public class GoofyEngine : IEngine
    {
        private GoofyCoreConfiguration _goofyCoreConfiguration;
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

        public IResourcesLoader ResourcesLoader { get; protected set; }

        public GoofyEngine(IResourcesLoader resourcesLoader)
        {
            ResourcesLoader = resourcesLoader;
        }

        public virtual void Start(IServiceCollection services)
        {
            /* Todas las tareas que se ejecutan a continuación se realizan luego de que los 
            ensamblados de los plugins son cargados */

            // Agregar las dependencias provistas por otros ensamblados(propios o de 3ros)
            RegisterDependencies(services);

            //Correr tareas de inicio provistas por otros ensamblados(propios o de 3ros)
            if (GoofyCoreConfiguration.RunStartupTasks)
            {
                var startupTasksTypes = ResourcesLoader.FindClassesOfType<IRunAtStartup>()
                                .Select(t => (IRunAtStartup)Activator.CreateInstance(t)).ToArray();
                var startupTasks = startupTasksTypes.AsQueryable().OrderBy(t => t.Order);
                foreach (var s in startupTasks)
                {
                    s.Run();
                }
            }
        }


        public void RegisterSortableDependencies<T>(Action<T> action) where T : ISortableTask
        {
            var depAssemblerTypes = ResourcesLoader.FindClassesOfType<T>()
                                                   .Select(t => (T)Activator.CreateInstance(t));
            var assemblers = depAssemblerTypes.AsQueryable().OrderBy(s => s.Order);
            foreach (var depAssembler in assemblers)
            {
                action(depAssembler);
            }
        }

        public void RegisterDependencies<T>(Action<T> action)
        {
            var depAssemblerTypes = ResourcesLoader.FindClassesOfType<T>()
                                                   .Select(t => (T)Activator.CreateInstance(t));
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
