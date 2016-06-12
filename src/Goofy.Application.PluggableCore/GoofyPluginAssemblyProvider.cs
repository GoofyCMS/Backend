using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Goofy.Application.PluggableCore
{
    public class GoofyPluginAssemblyProvider : IPluginAssemblyProvider
    {
        //private readonly string ComponentExtension = ".dll";
        private readonly IAssemblyLoaderContainer _assemblyLoaderContainer;
        private readonly IAssemblyLoadContextAccessor _assemblyLoadContextAccessor;
        private readonly ILogger<GoofyPluginAssemblyProvider> _logger;
        private IDictionary<string, IEnumerable<Assembly>> _pluginAssemblies;

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

        public IEnumerable<Assembly> Assemblies
        {
            get
            {
                if (_pluginAssemblies == null)
                {
                    LoadAssemblies();
                }
                return _pluginAssemblies.Values.SelectMany(vls => vls);
            }
        }

        public IDictionary<string, IEnumerable<Assembly>> PluginAssemblies
        {
            get
            {
                if (_pluginAssemblies == null)
                {
                    LoadAssemblies();
                }
                return _pluginAssemblies;
            }
        }

        protected void LoadAssemblies()
        {
            _pluginAssemblies = new Dictionary<string, IEnumerable<Assembly>>();
            if (Directory.Exists(PluginsDirectoryPath))
            {
                var assemblyLoadContext = _assemblyLoadContextAccessor.Default;
                using (_assemblyLoaderContainer.AddLoader(new GoofyPluginDirectoryLoader(assemblyLoadContext, PluginsDirectoryPath)))
                {
                    List<Assembly> assembliesPerPlugin;
                    foreach (var pluginFolder in Directory.EnumerateDirectories(PluginsDirectoryPath))
                    {
                        assembliesPerPlugin = new List<Assembly>();
                        foreach (var dll in Directory.EnumerateFiles(pluginFolder, "*.dll", SearchOption.TopDirectoryOnly))
                        {
                            var dllName = Path.GetFileNameWithoutExtension(dll);
                            assembliesPerPlugin.Add(assemblyLoadContext.Load(dllName));
                        }
                        _pluginAssemblies.Add(new DirectoryInfo(pluginFolder).Name, assembliesPerPlugin);
                    }
                }
            }
        }
    }
}
