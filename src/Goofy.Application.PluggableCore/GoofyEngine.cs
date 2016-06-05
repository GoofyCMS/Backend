﻿using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Goofy.Application.PluggableCore.Abstractions;
using System.Collections.Generic;
using Goofy.Infrastructure.Core.Adapter.Extensions;

namespace Goofy.Application.PluggableCore
{
    public class GoofyEngine : IEngine
    {
        protected IServiceCollection Services { get; private set; }
        protected IGoofyAssemblyProvider CoreAssembliesProvider { get; private set; }
        //protected GoofyCoreConfiguration _goofyCoreConfiguration;

        private readonly ILogger<GoofyEngine> _logger;

        //public GoofyCoreConfiguration GoofyCoreConfiguration
        //{
        //    get
        //    {
        //        if (_goofyCoreConfiguration == null)
        //        {
        //            _goofyCoreConfiguration = new GoofyCoreConfiguration();
        //        }
        //        return _goofyCoreConfiguration;
        //    }
        //}


        public GoofyEngine(
                           IServiceCollection services,
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
            //RunStartupTasks();
            //}

            // Eliminar servicios IDesignTimeService del contenedor de dependencias
            RemoveDesignTimeServices();
        }


        //private void ExecActionForeachType<T>(Action<T> action)
        //{
        /* 
            TODO: FIX THIS
        */
        //var depAssemblerTypes = _resourcesLoader.FindClassesOfType<T>()
        //                                       .Select(t => (T)ActivatorUtilities.CreateInstance(_services.BuildServiceProvider(), t));
        //foreach (var depAssembler in depAssemblerTypes)
        //{
        //    action(depAssembler);
        //}
        //}

        //protected virtual void RegisterDependencies()
        //    => ExecActionForeachSortableType<IDependencyRegistrar>(d =>
        //    {
        //        d.ConfigureServices(_services);
        //    });

        protected virtual void RegisterDependencies()
        {
            RegisterAdapterServices();
        }

        protected virtual void RegisterAdapterServices()
        {
            RegisterAdapterServices(CoreAssembliesProvider.GetAssemblies.Where(ass => ass.GetName().Name == "Goofy.Infrastructure.Plugins.Adapter").Concat(GetdditionalAdapterAssemblies()));
        }

        private void RegisterAdapterServices(IEnumerable<Assembly> assemblies)
        {
            Services.AddTypeAdpaterServices(assemblies);
        }

        protected virtual IEnumerable<Assembly> GetdditionalAdapterAssemblies()
        {
            return Enumerable.Empty<Assembly>();
        }

        //protected virtual void RunStartupTasks()
        //    => ExecActionForeachSortableType<IRunAtStartup>(startupTask => startupTask.Run());

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

        private bool ServiceIsDesignTimeService(ServiceDescriptor serviceDescriptor)
        {
            var serviceType = serviceDescriptor.ServiceType;
            if (serviceType != null)
                return typeof(IDesignTimeService).GetTypeInfo().IsAssignableFrom(serviceType.GetTypeInfo());
            return false;
        }
    }
}