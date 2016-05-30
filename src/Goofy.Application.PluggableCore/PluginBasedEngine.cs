using Goofy.Application.PluggableCore.Abstractions;
using Goofy.Application.PluggableCore.Services;
using Goofy.Domain.Core;
using Goofy.Domain.Core.Service.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Goofy.Application.PluggableCore
{
    public class PluginBasedEngine : GoofyEngine
    {
        private readonly IPluginAssemblyProvider _pluginAssemblyProvider;

        public PluginBasedEngine(
                                 IServiceCollection services,
                                 IGoofyAssemblyProvider coreAssembliesProvider,
                                 IPluginAssemblyProvider pluginAssemblyProvider,
                                 ILogger<PluginBasedEngine> logger
                                )
            : base(services, coreAssembliesProvider, logger)
        {
            _pluginAssemblyProvider = pluginAssemblyProvider;
        }

        protected override void RegisterDependencies()
        {
            base.RegisterDependencies();
            var assembly = CoreAssembliesProvider.GetAssemblies.Where(ass => ass.GetName().Name == "Goofy.Infrastructure.Plugins.Adapter").FirstOrDefault();
            RegisterAdapterServices(_pluginAssemblyProvider.GetAssemblies.Concat(CoreAssembliesProvider.GetAssemblies.Where(ass => ass.GetName().Name == "Goofy.Infrastructure.Plugins.Adapter")));
            RegisterUnitOfWorkObjects(_pluginAssemblyProvider.GetAssemblies.Concat(CoreAssembliesProvider.GetAssemblies.Where(ass => ass.GetName().Name == "Goofy.Infrastructure.Plugins.Data")));
            RegisterPluginManager();
        }

        private void RegisterUnitOfWorkObjects(IEnumerable<Assembly> assemblies)
        {
            var contextTypes = assemblies.FindClassesOfType<IUnitOfWork>();
            foreach (var context in contextTypes)
            {
                Services.AddSingleton(context);
            }
        }

        private void RegisterPluginManager()
        {
            var pluginManager = new PluginManager(_pluginAssemblyProvider);
            Services.AddInstance<IPluginManager>(pluginManager);
        }
    }
}
