using System;
using System.Linq;
using Goofy.Core.Configuration;
using Goofy.Configuration.Extensions;

namespace Goofy.Core.Infrastructure
{
    public abstract class GoofyEngine : IEngine
    {
        private static GoofyCoreConfiguration _goofyCoreConfiguration;
        public static GoofyCoreConfiguration GoofyCoreConfiguration
        {
            get
            {
                if (_goofyCoreConfiguration == null)
                {
                    string jsonFilePath = string.Format("{0}\\{1}", GoofyDomainResourceLocator.GetBinDirectoryPath(), ConfigurationSource);
                    _goofyCoreConfiguration = ConfigurationExtensions.GetConfiguration<GoofyCoreConfiguration>(jsonFilePath, ConfigurationSection);
                }
                return _goofyCoreConfiguration;
            }
        }

        public IResourcesLoader ResourcesLoader { get; protected set; }

        public GoofyEngine()
        {
            //Registrar el un IResourcesLoader
            ResourcesLoader = new GoofyResourcesLoader(GoofyCoreConfiguration);
        }

        public virtual void Start(IDependencyContainer dependencyContainer)
        {
            //dependencyContainer.RegisterInstanceAsSingleton(ResourcesLoader);

            /* Todas las tareas que se ejecutan a continuación se realizan luego de que los 
            ensamblados de los plugins son cargados */

            // Agregar las dependencias provistas por otros ensamblados(propios o de 3ros)
            RegisterDependencies(dependencyContainer);

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


        public void RegisterSortableDependencies<T>(IDependencyContainer dependencyContainer, Action<T> action) where T : ISortableTask
        {
            var depAssemblerTypes = ResourcesLoader.FindClassesOfType<T>()
                                                   .Select(t => (T)Activator.CreateInstance(t));
            var assemblers = depAssemblerTypes.AsQueryable().OrderBy(s => s.Order);
            foreach (var depAssembler in assemblers)
            {
                action(depAssembler);
            }
        }

        public void RegisterDependencies<T>(IDependencyContainer dependencyContainer, Action<T> action)
        {
            var depAssemblerTypes = ResourcesLoader.FindClassesOfType<T>()
                                                   .Select(t => (T)Activator.CreateInstance(t));
            foreach (var depAssembler in depAssemblerTypes)
            {
                action(depAssembler);
            }
        }

        public virtual void RegisterDependencies(IDependencyContainer dependencyContainer)
        {
            RegisterSortableDependencies<IDependencyAssembler>(dependencyContainer,
                                                                    d =>
                                                                    {
                                                                        d.Register(dependencyContainer, ResourcesLoader);
                                                                    }
                                                               );
        }


        #region Properties

        public static string ConfigurationSource
        {
            get
            {
                return @"Goofy.Core\config.json";
            }
        }

        public static string ConfigurationSection
        {
            get
            {
                return "GoofyCoreSection";
            }
        }

        #endregion
    }
}
