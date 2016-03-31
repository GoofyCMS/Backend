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
        
        protected virtual HashSet<string> ReferenceAssemblies { get; } = new HashSet<string>()
        {
            "Goofy.Core",
            "Goofy.Data",
            "Goofy.Core.WebFramework",
            "Goofy.Data.WebFramework",
            "Goofy.WebFramework",
            "Goofy.WebApi"
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
        /*
            TODO
            Esto hay que arreglarlo, está cableado porque no me funciona la forma de trabajar con
            la instancia ILibraryManager.
        */
            
        //    return _libraryManager.GetLibraries().Where(IsGoofyLibrary).Select(s => Assembly.Load(new AssemblyName(s.Name)));
            var a = ReferenceAssemblies.Select(assemblyName => Assembly.Load(new AssemblyName(assemblyName)))
                                      .Union(_componentsAssembliesProvider.ComponentsAssemblies);
            foreach (var l in a)
            {
                _logger.LogInformation("System assembly {0}.", l);
            }
            return ReferenceAssemblies.Select(assemblyName => Assembly.Load(new AssemblyName(assemblyName)))
                                      .Union(_componentsAssembliesProvider.ComponentsAssemblies);
        }

        // private IEnumerable<string> GetFrameworkLibraries()
        // {
            // if (ReferenceAssemblies == null)
            // {
            //     return Enumerable.Empty<string>();
            // }
            // var a = ReferenceAssemblies.SelectMany(_libraryManager.GetLibraries(IsGoofyLibrary));
            // foreach (var l in a)
            // {
            //     _logger.LogInformation("Ref {0}", l);
            // }
            // return ReferenceAssemblies.SelectMany(_libraryManager.GetReferencingLibraries)
            //                           .Select(s => s.Name)
            //                           .Distinct();
            
                                    //   .Select(l => l.Name);
                                    //   .Where(IsCandidateLibrary);
            
        // }
    }
}
