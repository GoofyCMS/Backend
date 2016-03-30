using System;
using System.Reflection;
using Microsoft.Extensions.PlatformAbstractions;

namespace Goofy.Core.Components
{
    public class GoofyPlatformAbstractionsAssemblyLoader : IAssemblyLoader
    {
        private readonly IAssemblyLoadContext _assemblyLoadContext;

        public GoofyPlatformAbstractionsAssemblyLoader(IAssemblyLoadContext assemblyLoadContext)
        {
            _assemblyLoadContext = assemblyLoadContext;
        }

        public Assembly Load(AssemblyName assemblyName)
        {
            return _assemblyLoadContext.Load(assemblyName);
        }

        public IntPtr LoadUnmanagedLibrary(string name)
        {
            return _assemblyLoadContext.LoadUnmanagedLibrary(name);
        }
    }
}
