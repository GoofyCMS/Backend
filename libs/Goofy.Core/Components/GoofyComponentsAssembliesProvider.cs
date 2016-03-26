using System.Collections.Generic;
using System.Reflection;
using Goofy.Core.Components.Base;
using System.IO;
using System;

namespace Goofy.Core.Components
{
    public class GoofyComponentsAssembliesProvider : IComponentsAssembliesProvider
    {
        private const string ComponentDirectoryPrefixPattern = "Goofy.Component.*";
        private readonly IComponentsDirectoryPathProvider _componentsPathProvider;
        private readonly string ComponentExtension = ".dll";
        private List<Assembly> _componentsAssemblies;


        public GoofyComponentsAssembliesProvider(IComponentsDirectoryPathProvider componentsPathProvider)
        {
            _componentsPathProvider = componentsPathProvider;
        }

        public IEnumerable<Assembly> ComponentsAssemblies
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

        public void LoadAssemblies()
        {
            _componentsAssemblies = new List<Assembly>();
            var componentsDirectoryPath = _componentsPathProvider.GetComponentsDirectoryPath();
            foreach (var componentPath in Directory.GetDirectories(componentsDirectoryPath, ComponentDirectoryPrefixPattern, SearchOption.TopDirectoryOnly))
            {
                var dllPath = string.Format("{0}\\{1}{2}", componentPath, new DirectoryInfo(componentPath).Name, ComponentExtension);
                Assembly currentAssembly;
                AssemblyName assemblyName;
                try
                {
                    assemblyName = AssemblyName.GetAssemblyName(dllPath);
                    currentAssembly = Assembly.Load(assemblyName);//cambiar esta línea por appDomain.Load(assemblyName) en caso de que se quiera cargar el assembly al dominio vigente
                    _componentsAssemblies.Add(currentAssembly);
                }
                catch (TargetInvocationException e)
                {
                    //TODO:agregar a los logs ComponenteInválida porque no presenta una dll.
                    continue;
                }
            }
        }
    }
}
