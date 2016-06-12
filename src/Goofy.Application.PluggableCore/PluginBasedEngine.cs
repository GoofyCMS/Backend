using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Goofy.Application.PluggableCore.Abstractions;
using Goofy.Application.PluggableCore.Extensions;

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
            //AddPluginDependencies();
        }

        protected override IEnumerable<Assembly> GetExtensionAssemblies()
        {
            return _pluginManager.PluginAssemblyProvider.Assemblies;
        }

        protected override IEnumerable<Assembly> GetdditionalAdapterAssemblies()
        {
            return _pluginManager.GetInfrastructureAdapterAssemblies();
        }

        protected override IEnumerable<Assembly> GetAdditionalDependencyRegistrarAssemblies()
        {
            return _pluginManager.GetAssembliesPerLayer(AppLayer.Application, AppLayer.Presentation);
        }
    }
}
