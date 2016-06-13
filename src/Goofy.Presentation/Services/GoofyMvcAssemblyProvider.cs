using Goofy.Application.Administration.Services.Abstractions;
using Goofy.Application.Core;
using Goofy.Application.Core.Abstractions;
using Goofy.Application.Core.Extensions;
using Microsoft.AspNet.Mvc.Infrastructure;
using Microsoft.Extensions.PlatformAbstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Goofy.Presentation.Services
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
            return _pluginManager.PluginAssemblyProvider.Assemblies.GetAssembliesPerLayer(AppLayer.Presentation)
                                 .Concat(_systemAssembliesProvider.Assemblies.GetAssembliesPerLayer(AppLayer.Presentation))
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
