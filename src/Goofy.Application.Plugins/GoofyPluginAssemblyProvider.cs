using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.Extensions.PlatformAbstractions;
using Goofy.Domain.Plugins;
using Microsoft.Extensions.Logging;

namespace Goofy.Application.Plugins
{
    public class GoofyPluginAssemblyProvider : IPluginAssemblyProvider
    {
        //private readonly string ComponentExtension = ".dll";
        private readonly IAssemblyLoaderContainer _assemblyLoaderContainer;
        private readonly IAssemblyLoadContextAccessor _assemblyLoadContextAccessor;
        private readonly ILogger<GoofyPluginAssemblyProvider> _logger;
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
                                           IAssemblyLoadContextAccessor assemblyLoadContextAccessor,
                                           ILogger<GoofyPluginAssemblyProvider> logger
                                               )
        {
            _assemblyLoaderContainer = assemblyLoaderContainer;
            _assemblyLoadContextAccessor = assemblyLoadContextAccessor;
            _logger = logger;
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
            foreach (var pluginFolder in Directory.EnumerateDirectories(PluginsDirectoryPath))
            {
                using (_assemblyLoaderContainer.AddLoader(new GoofyPluginDirectoryLoader(assemblyLoadContext, pluginFolder)))
                {
                    foreach (var dll in Directory.EnumerateFiles(pluginFolder, "*.dll", SearchOption.TopDirectoryOnly))
                    {
                        var dllName = Path.GetFileNameWithoutExtension(dll);
                        _componentsAssemblies.Add(assemblyLoadContext.Load(dllName));
                    }
                }
            }
        }
    }
}
