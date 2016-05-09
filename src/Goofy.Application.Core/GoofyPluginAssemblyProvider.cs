using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.Extensions.PlatformAbstractions;
using Goofy.Domain.Core.Abstractions;

namespace Goofy.Application.Core
{
    public class GoofyPluginAssemblyProvider : IPluginAssemblyProvider
    {
        private readonly string ComponentExtension = ".dll";
        private readonly IAssemblyLoaderContainer _assemblyLoaderContainer;
        private readonly IAssemblyLoadContextAccessor _assemblyLoadContextAccessor;
        //private readonly ILogger<GoofyPluginAssemblyProvider> _logger;
        private List<Assembly> _componentsAssemblies;

        protected string _pluginDirectoryName = "plugins";


        public virtual string PluginsDirectoryPath
        {
            get
            {
                return Path.Combine(Directory.GetCurrentDirectory(), _pluginDirectoryName);
            }
            protected set
            {
                _pluginDirectoryName = value;
            }
        }


        public GoofyPluginAssemblyProvider(IAssemblyLoaderContainer assemblyLoaderContainer,
                                           IAssemblyLoadContextAccessor assemblyLoadContextAccessor
                                               //ILogger<GoofyPluginAssemblyProvider> logger
                                               )
        {
            _assemblyLoaderContainer = assemblyLoaderContainer;
            _assemblyLoadContextAccessor = assemblyLoadContextAccessor;
            //_logger = logger;
        }

        public IEnumerable<Assembly> GetAssemblies
        {
            get
            {
                if (_componentsAssemblies == null)
                {
                    LoadAssemblies();
                }
                return _componentsAssemblies;
            }
        }

        protected void LoadAssemblies()
        {
            _componentsAssemblies = new List<Assembly>();

            var assemblyLoadContext = _assemblyLoadContextAccessor.Default;
            using (_assemblyLoaderContainer.AddLoader(new GoofyPluginDirectoryLoader(assemblyLoadContext, PluginsDirectoryPath)))
            {
                foreach (var componentDirectory in Directory.EnumerateDirectories(PluginsDirectoryPath).Select(dir => new DirectoryInfo(dir)).Reverse())
                {

                    var componentName = componentDirectory.Name;
                    var componentDllPath = Path.Combine(componentDirectory.FullName, string.Format("{0}{1}", componentName, ComponentExtension));
                    if (File.Exists(componentDllPath))
                    {
                        var dllName = Path.GetFileNameWithoutExtension(componentDllPath);
                        _componentsAssemblies.Add(assemblyLoadContext.Load(dllName));
                        //_logger.LogInformation("La Componente \"{0}\", fue cargada satisfactoriamente.", componentName);
                    }
                    else
                    {
                        //_logger.LogWarning("La carpeta \"{0}\" situada en \"{1}\" no es un Component válida.", componentName, componentDirectory.FullName);
                    }
                }
            }
        }
    }
}
