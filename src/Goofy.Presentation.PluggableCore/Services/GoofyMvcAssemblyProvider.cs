using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNet.Mvc.Infrastructure;
using Goofy.Application.PluggableCore;

namespace Goofy.WebFramework.Mvc
{
    public class GoofyMvcAssemblyProvider : DefaultAssemblyProvider
    {
        private readonly IPluginAssemblyProvider _pluginAssemblyProvider;
        private readonly string[] _referenceAssemblies;

        public GoofyMvcAssemblyProvider(
                                         ILibraryManager libraryManager,
                                         IPluginAssemblyProvider pluginAssemblyProvider,
                                         string[] referenceAssemblies = null
                                        )
            : base(libraryManager)
        {
            _pluginAssemblyProvider = pluginAssemblyProvider;
            _referenceAssemblies = referenceAssemblies;
        }

        protected override HashSet<string> ReferenceAssemblies
            => _referenceAssemblies == null
                ? base.ReferenceAssemblies
                : new HashSet<string>(_referenceAssemblies);

        protected override IEnumerable<Library> GetCandidateLibraries()
        {
            return _pluginAssemblyProvider.GetAssemblies.Concat(new[] { GetType().Assembly }).Select(
                    x => new Library(x.FullName, null, null, null, Enumerable.Empty<string>(),
                        new[] { new AssemblyName(x.FullName) }));
        }

        private string PluginPath(Assembly assembly)
        {
            var assemblyName = assembly.GetName().Name;
            return Path.Combine(_pluginAssemblyProvider.PluginsDirectoryPath, assemblyName, assemblyName + ".dll");
        }
    }
}
