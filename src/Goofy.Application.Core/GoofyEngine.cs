using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Goofy.Domain.Core.Abstractions;
using Goofy.Domain.Core.Service.Data;

namespace Goofy.Application.Core
{
    public class GoofyEngine : IEngine
    {
        //protected GoofyCoreConfiguration _goofyCoreConfiguration;
        protected readonly IServiceCollection _services;
        //private readonly IGoofyAssemblyProvider _coreAssemblies;
        private readonly IPluginAssemblyProvider _pluginAssemblies;

        //private readonly ILogger<GoofyEngine> _logger;

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
                           //IGoofyAssemblyProvider coreAssemblies,
                           IPluginAssemblyProvider pluginAssemblies
                                                            /*ILogger<GoofyEngine> logger*/)
        {
            _services = services;
            //_coreAssemblies = coreAssemblies;
            _pluginAssemblies = pluginAssemblies;
            //_logger = logger;
        }

        public virtual IServiceProvider Start()
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

            IServiceProvider serviceProvider = _services.BuildServiceProvider();
            return serviceProvider;
        }


        private void ExecActionForeachSortableType<T>(Action<T> action) where T : ISortableTask
        {
            /* 
               TODO: FIX THIS
           */
            //var depAssemblerTypes = _resourcesLoader.FindClassesOfType<T>()
            //                                       .Select(t => (T)ActivatorUtilities.CreateInstance(_services.BuildServiceProvider(), t));
            //var assemblers = depAssemblerTypes.OrderBy(s => s.Order);
            //foreach (var depAssembler in assemblers)
            //{
            //    action(depAssembler);
            //}
        }

        private void ExecActionForeachType<T>(Action<T> action)
        {
            /* 
                TODO: FIX THIS
            */
            //var depAssemblerTypes = _resourcesLoader.FindClassesOfType<T>()
            //                                       .Select(t => (T)ActivatorUtilities.CreateInstance(_services.BuildServiceProvider(), t));
            //foreach (var depAssembler in depAssemblerTypes)
            //{
            //    action(depAssembler);
            //}
        }

        //protected virtual void RegisterDependencies()
        //    => ExecActionForeachSortableType<IDependencyRegistrar>(d =>
        //    {
        //        d.ConfigureServices(_services);
        //    });
        protected virtual void RegisterDependencies()
        {
            //var coreAssemblies = _coreAssemblies.GetAssemblies;
            var contextTypes = _pluginAssemblies.GetAssemblies.FindClassesOfType<IUnitOfWork>();
            foreach (var context in contextTypes)
            {
                _services.AddSingleton(context);
            }
        }


        protected virtual void RunStartupTasks()
            => ExecActionForeachSortableType<IRunAtStartup>(startupTask => startupTask.Run());

        protected virtual void RemoveDesignTimeServices()
        {
            var servicesForDeleting = _services.Where(ServiceIsDesignTimeService).ToArray();
            foreach (var serviceDescriptor in servicesForDeleting)
            {
                _services.Remove(serviceDescriptor);
                /* TODO: Chequear que no da NullReferenceException si el servicio se registró
                    usando un Factory
                */
                //var implementationType = serviceDescriptor.ImplementationType ?? serviceDescriptor.ImplementationInstance.GetType();
                //_logger.LogInformation(string.Format("Remove implementationType \"{0}\", for service \"{1}\".", implementationType, serviceDescriptor.ServiceType));
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
