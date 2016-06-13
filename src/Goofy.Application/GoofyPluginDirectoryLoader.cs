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

        public GoofyPluginDirectoryLoader(IAssemblyLoadContext assemblyLoadContext, string pluginDirectoryPath)
        {
            _assemblyLoadContext = assemblyLoadContext;
            _pluginDirectoryPath = pluginDirectoryPath;
        }

        public Assembly Load(AssemblyName assemblyName)
        {
            /*
                Fix this for 
            */
            return _assemblyLoadContext.LoadFile(Path.Combine(_pluginDirectoryPath, "Blog", assemblyName.Name + ".dll"));
        }

        public IntPtr LoadUnmanagedLibrary(string name)
        {
            throw new NotImplementedException();
        }
    }
}
