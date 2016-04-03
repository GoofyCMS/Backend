using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Logging;
using Goofy.Core.Components.Base;

namespace Goofy.Core.Infrastructure
{
    public class GoofyAssembliesProvider : IAssembliesProvider
    {
        private IComponentsAssembliesProvider _componentsAssembliesProvider;
        private readonly ILibraryManager _libraryManager;
        private readonly ILogger<GoofyAssembliesProvider> _logger;
        private static IEnumerable<Assembly> _assemblies;

        protected virtual HashSet<string> ReferenceAssemblies { get; } = new HashSet<string>()
        {
            "Goofy.Core"
        };

        public GoofyAssembliesProvider(ILibraryManager libraryManager,
                                       IComponentsAssembliesProvider componentsAssembliesProvider,
                                       ILogger<GoofyAssembliesProvider> logger)
        {
            _libraryManager = libraryManager;
            _componentsAssembliesProvider = componentsAssembliesProvider;
            _logger = logger;
        }


        public IEnumerable<Assembly> GetAssemblies()
        {
            if (_assemblies == null)
            {
                _assemblies = LoadAssemblies();
                foreach (var l in _assemblies)
                {
                    _logger.LogInformation("System assembly {0}.", l);
                }
            }
            return _assemblies;
        }

        private IEnumerable<Assembly> LoadAssemblies()
        {
            //Aquí no se incluye, el assembly Goofy.Core
            var assemblies = ReferenceAssemblies.SelectMany(_libraryManager.GetReferencingLibraries)
                                                .Distinct()
                                                .Select(assDesc => Assembly.Load(new AssemblyName(assDesc.Name)))
                                                .Union(_componentsAssembliesProvider.ComponentsAssemblies)
                                                .ToArray();
            return assemblies;
        }
    }
}
