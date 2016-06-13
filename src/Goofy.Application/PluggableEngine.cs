using Goofy.Application.Core;
using Goofy.Application.Core.Abstractions;
using Goofy.Application.Core.Extensions;
using Goofy.Application.Core.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Reflection;

namespace Goofy.Application
{
    public class PluggableEngine : GoofyEngine
    {
        private readonly IPluginAssemblyProvider _pluginAssemblyProvider;

        public PluggableEngine(
                                 IServiceCollection services,
                                 IGoofyAssemblyProvider coreAssembliesProvider,
                                 ILogger<PluggableEngine> logger,
                                 IPluginAssemblyProvider pluginAssemblyProvider
                                )
            : base(services, coreAssembliesProvider, logger)
        {
            _pluginAssemblyProvider = pluginAssemblyProvider;
        }

        protected override void RegisterDependencies()
        {
            base.RegisterDependencies();
        }

        protected override IEnumerable<Assembly> GetExtensionAssemblies()
        {
            return _pluginAssemblyProvider.Assemblies;
        }

        protected override IEnumerable<Assembly> GetdditionalAdapterAssemblies()
        {
            return _pluginAssemblyProvider.Assemblies.GetInfrastructureAdapterAssemblies();
        }

        protected override IEnumerable<Assembly> GetAdditionalDependencyRegistrarAssemblies()
        {
            return _pluginAssemblyProvider.Assemblies.GetAssembliesPerLayer(AppLayer.Application, AppLayer.Presentation);
        }
    }
}
