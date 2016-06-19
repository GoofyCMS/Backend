using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.IO;
using System.Reflection;

namespace Goofy.Application
{
    public class GoofyPluginDirectoryLoader : IAssemblyLoader
    {
        private readonly IAssemblyLoadContext _assemblyLoadContext;
        private readonly string _pluginDirectoryPath;
        private readonly string _pluginFolder;

        public GoofyPluginDirectoryLoader(IAssemblyLoadContext assemblyLoadContext, string pluginDirectoryPath, string pluginFolder)
        {
            _assemblyLoadContext = assemblyLoadContext;
            _pluginDirectoryPath = pluginDirectoryPath;
            _pluginFolder = pluginFolder;
        }

        public Assembly Load(AssemblyName assemblyName)
        {
            /*
                Fix this for 
            */
            return _assemblyLoadContext.LoadFile(Path.Combine(_pluginDirectoryPath, _pluginFolder, assemblyName.Name + ".dll"));
        }

        public IntPtr LoadUnmanagedLibrary(string name)
        {
            throw new NotImplementedException();
        }

    }
}
