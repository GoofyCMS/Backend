using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.PlatformAbstractions;

namespace Goofy.Application.Plugins
{
    public class GoofyPluginDirectoryLoader : IAssemblyLoader
    {
        private readonly IAssemblyLoadContext _assemblyLoadContext;
        private readonly string _pluginDirectoryPath;
                
        public GoofyPluginDirectoryLoader(IAssemblyLoadContext assemblyLoadContext, string componentsDirectoryPath)
        {
            _assemblyLoadContext = assemblyLoadContext;
            _pluginDirectoryPath = componentsDirectoryPath;
        }

        public Assembly Load(AssemblyName assemblyName)
        {
            return _assemblyLoadContext.LoadFile(Path.Combine(_pluginDirectoryPath, assemblyName.Name + ".dll"));
        }

        public IntPtr LoadUnmanagedLibrary(string name)
        {
            throw new NotImplementedException();
        }
    }
}
