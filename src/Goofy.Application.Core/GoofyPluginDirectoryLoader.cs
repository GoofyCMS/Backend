using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.PlatformAbstractions;

namespace Goofy.Application.Core
{
    public class GoofyPluginDirectoryLoader : IAssemblyLoader
    {
        private readonly IAssemblyLoadContext _assemblyLoadContext;
        private readonly string _componentsDirectoryPath;
                
        public GoofyPluginDirectoryLoader(IAssemblyLoadContext assemblyLoadContext, string componentsDirectoryPath)
        {
            _assemblyLoadContext = assemblyLoadContext;
            _componentsDirectoryPath = componentsDirectoryPath;
        }

        public Assembly Load(AssemblyName assemblyName)
        {
            return _assemblyLoadContext.LoadFile(Path.Combine(_componentsDirectoryPath, assemblyName.Name, assemblyName.Name + ".dll"));
        }

        public IntPtr LoadUnmanagedLibrary(string name)
        {
            throw new NotImplementedException();
        }
    }
}
