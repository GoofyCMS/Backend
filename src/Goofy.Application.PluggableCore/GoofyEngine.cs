using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Goofy.Application.PluggableCore.Abstractions;
using System.Collections.Generic;
using Goofy.Infrastructure.Core.Adapter.Extensions;
using Goofy.Domain.Core;
using Goofy.Application.PluggableCore.Extensions;

namespace Goofy.Application.PluggableCore
{
    public class GoofyEngine : IEngine
    {
        protected IServiceCollection Services { get; private set; }
        protected IGoofyAssemblyProvider CoreAssembliesProvider { get; private set; }

        private readonly ILogger<GoofyEngine> _logger;

        public GoofyEngine(IServiceCollection services,
                           IGoofyAssemblyProvider coreAssembliesProvider,
                           ILogger<GoofyEngine> logger)
        {
            Services = services;
            CoreAssembliesProvider = coreAssembliesProvider;
            _logger = logger;
        }

        public virtual void Start()
        {
            // Agregar las dependencias provistas por otros ensamblados(propios o de 3ros)
            RegisterDependencies();

            //Correr tareas de inicio provistas por otros ensamblados(propios o de 3ros)
            //if (GoofyCoreConfiguration.RunStartupTasks)
            //{
            RunStartupTasks();
            //}

            // Eliminar servicios IDesignTimeService del contenedor de dependencias
            RemoveDesignTimeServices();

            //Eliminar el servicio IServiceCollection
            RemoveServiceCollection();
        }

        protected virtual void RegisterDependencies()
        {
            RegisterAdapterServices();
            RunDepedencyRegistrarTasks();
        }

        #region Run DependencyRegistrar Tasks

        protected virtual void RunDepedencyRegistrarTasks()
        {
            RunDependencyRegistrarTasks(CoreAssembliesProvider.Assemblies
                                                              .GetAssembliesPerLayer(AppLayer.Application, AppLayer.Presentation)
                                                              .Concat(GetAdditionalDependencyRegistrarAssemblies()));
        }

        protected virtual void RunDependencyRegistrarTasks(IEnumerable<Assembly> assemblies)
        {
            foreach (var registrarClass in assemblies.FindClassesOfType<IDependencyRegistrar>())
            {
                var dependencyRegistrarClass = (IDependencyRegistrar)Activator.CreateInstance(registrarClass);
                dependencyRegistrarClass.ConfigureServices(Services, CoreAssembliesProvider.Assemblies.Concat(GetExtensionAssemblies()));
            }
        }

        protected virtual IEnumerable<Assembly> GetExtensionAssemblies()
        {
            return Enumerable.Empty<Assembly>();
        }

        protected virtual IEnumerable<Assembly> GetAdditionalDependencyRegistrarAssemblies()
        {
            return Enumerable.Empty<Assembly>();
        }


        #endregion


        #region Register Adapters 

        protected virtual void RegisterAdapterServices()
        {
            RegisterAdapterServices(CoreAssembliesProvider.Assemblies.Where(IsAdapterAssembly).Concat(GetdditionalAdapterAssemblies()));
        }

        private bool IsAdapterAssembly(Assembly assembly)
        {
            var assemblyName = assembly.GetName().Name;
            return assemblyName.StartsWith("Goofy.Infrastructure.") && assemblyName.EndsWith(".Adapter");
        }

        private void RegisterAdapterServices(IEnumerable<Assembly> assemblies)
        {
            Services.AddTypeAdpaterServices(assemblies);
        }

        protected virtual IEnumerable<Assembly> GetdditionalAdapterAssemblies()
        {
            return Enumerable.Empty<Assembly>();
        }

        #endregion


        #region Run Startup Tasks

        protected virtual void RunStartupTasks()
        {
            var serviceProvider = Services.BuildServiceProvider();
            foreach (var startupTaskType in CoreAssembliesProvider.Assemblies
                                                                  .Concat(GetAdditonalStartupAssemblies())
                                                                  .FindClassesOfType<IRunAtStartup>()
                                                                  .Select(taskType => ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, taskType))
                                                                  .Cast<IRunAtStartup>()
                                                                  .OrderBy(s => s.Order))
            {
                startupTaskType.Run();
            }
        }

        protected virtual IEnumerable<Assembly> GetAdditonalStartupAssemblies()
        {
            return Enumerable.Empty<Assembly>();
        }

        protected virtual void RemoveDesignTimeServices()
        {
            var servicesForDeleting = Services.Where(ServiceIsDesignTimeService).ToArray();
            foreach (var serviceDescriptor in servicesForDeleting)
            {
                Services.Remove(serviceDescriptor);
                /* TODO: Chequear que no da NullReferenceException si el servicio se registró
                    usando un Factory
                */
                var implementationType = serviceDescriptor.ImplementationType ?? serviceDescriptor.ImplementationInstance.GetType();
                _logger.LogInformation(string.Format("Remove implementationType \"{0}\", for service \"{1}\".", implementationType, serviceDescriptor.ServiceType));
            }
        }

        #endregion

        private bool ServiceIsDesignTimeService(ServiceDescriptor serviceDescriptor)
        {
            var serviceType = serviceDescriptor.ServiceType;
            if (serviceType != null)
                return typeof(IDesignTimeService).GetTypeInfo().IsAssignableFrom(serviceType.GetTypeInfo());
            return false;
        }

        private void RemoveServiceCollection()
        {
            var serviceDescriptor = Services.Where(sd => sd.ServiceType == typeof(IServiceCollection)).First();
            Services.Remove(serviceDescriptor);
        }

    }
}
