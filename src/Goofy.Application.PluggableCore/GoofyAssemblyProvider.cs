using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Logging;
using Goofy.Application.PluggableCore.Abstractions;

namespace Goofy.Application.PluggableCore
{
    public class GoofyAssemblyProvider : IGoofyAssemblyProvider
    {
        private readonly ILibraryManager _libraryManager;
        private readonly ILogger<GoofyAssemblyProvider> _logger;
        private IEnumerable<Assembly> _assemblies;

        protected virtual HashSet<string> ReferenceAssemblies { get; } = new HashSet<string>()
        {
            "Goofy.Domain.Core"
        };

        public GoofyAssemblyProvider(ILibraryManager libraryManager,
                                     ILogger<GoofyAssemblyProvider> logger)
        {
            _libraryManager = libraryManager;
            _logger = logger;
        }


        public IEnumerable<Assembly> GetAssemblies
        {
            get
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
        }

        private IEnumerable<Assembly> LoadAssemblies()
        {
            var assemblies = ReferenceAssemblies.Select(_libraryManager.GetLibrary)
                                                .Union(ReferenceAssemblies.SelectMany(_libraryManager.GetReferencingLibraries))
                                                .Distinct()
                                                .Select(assDesc => Assembly.Load(new AssemblyName(assDesc.Name)))
                                                .ToArray();
            return assemblies;
        }
    }
}
