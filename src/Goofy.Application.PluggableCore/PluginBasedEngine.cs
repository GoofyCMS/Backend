using Goofy.Application.PluggableCore.Abstractions;
using Goofy.Application.PluggableCore.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;
using Goofy.Domain.Core;

namespace Goofy.Application.PluggableCore
{
    public class PluginBasedEngine : GoofyEngine
    {
        private readonly IPluginManager _pluginManager;

        public PluginBasedEngine(
                                 IServiceCollection services,
                                 IGoofyAssemblyProvider coreAssembliesProvider,
                                 ILogger<PluginBasedEngine> logger,
                                 IPluginManager pluginManager
                                )
            : base(services, coreAssembliesProvider, logger)
        {
            _pluginManager = pluginManager;
        }

        protected override void RegisterDependencies()
        {
            base.RegisterDependencies();
            //Buscar todos los de aplicación y ejecutar los IDependencyRegistrar
            AddPluginDependencies();
        }

        private void AddPluginDependencies()
        {
            foreach (var registrarClass in _pluginManager.GetAssembliesPerLayer(AppLayer.Application).FindClassesOfType<IDependencyRegistrar>())
            {
                var dependencyRegistrarClass = (IDependencyRegistrar)Activator.CreateInstance(registrarClass);
                dependencyRegistrarClass.ConfigureServices(Services);
            }

            //AddExtraDependencies
            foreach (var dependenciesAdder in Services.Where(s => s.ServiceType != null && s.ServiceType == typeof(PluginDependenciesAdder)).Select(s => s.ImplementationType).ToArray())
            {
                var dependenciesAdderInstance = (PluginDependenciesAdder)Activator.CreateInstance(dependenciesAdder);
                dependenciesAdderInstance.AddPluginExtraDependencies(Services, _pluginManager);
            }
        }

        protected override IEnumerable<Assembly> GetdditionalAdapterAssemblies()
        {
            return _pluginManager.GetInfrastructureAdapterAssemblies();
        }
    }
}
