using System;
using System.Reflection;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;

namespace Goofy.Core.Infrastructure
{
    public class AppDomainResourcesLocator : IResourcesLocator
    {
        protected readonly ILibraryManager _libraryManager;
        protected readonly ILogger<AppDomainResourcesLocator> _logger;
        public AppDomainResourcesLocator(ILibraryManager libraryManager, ILogger<AppDomainResourcesLocator> logger)
        {
            _libraryManager = libraryManager;
            _logger = logger;
            _logger.LogInformation("AppDomainResourcesLocator constructor was called");
        }
        public virtual Assembly[] GetAssemblies()
        {
            //var reflectinOnlyAssemblies = AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies();
            //string a = reflectinOnlyAssemblies.ToString();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return assemblies;
        }

        public virtual string GetBinDirectoryPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
