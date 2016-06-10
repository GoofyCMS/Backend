using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNet.Mvc.Infrastructure;
using Goofy.Application.PluggableCore.Abstractions;
using Goofy.Application.PluggableCore.Extensions;

namespace Goofy.WebFramework.Mvc
{
    public class GoofyMvcAssemblyProvider : DefaultAssemblyProvider
    {
        private readonly IPluginManager _pluginManager;
        private readonly string[] _referenceAssemblies;
        private readonly IGoofyAssemblyProvider _systemAssembliesProvider;

        public GoofyMvcAssemblyProvider(
                                         ILibraryManager libraryManager,
                                         IPluginManager pluginManager,
                                         IGoofyAssemblyProvider systemAssembliesProvider,
                                         string[] referenceAssemblies = null
                                        )
            : base(libraryManager)
        {
            _pluginManager = pluginManager;
            _referenceAssemblies = referenceAssemblies;
            _systemAssembliesProvider = systemAssembliesProvider;
        }

        protected override HashSet<string> ReferenceAssemblies
            => _referenceAssemblies == null
                ? base.ReferenceAssemblies
                : new HashSet<string>(_referenceAssemblies);

        protected override IEnumerable<Library> GetCandidateLibraries()
        {
            return _pluginManager.GetAssembliesPerLayer(AppLayer.Presentation)
                                 .Concat(_systemAssembliesProvider.GetAssemblies.GetAssembliesPerLayer(AppLayer.Presentation))
                                 .Select(x =>
                                 {
                                     return new Library(x.FullName, null, null, null, Enumerable.Empty<string>(),
                                                        new[] { new AssemblyName(x.FullName) });
                                 });
        }

        private string PluginPath(Assembly assembly)
        {
            var assemblyName = assembly.GetName().Name;
            return Path.Combine(_pluginManager.PluginAssemblyProvider.PluginsDirectoryPath, assemblyName, assemblyName + ".dll");
        }
    }
}
