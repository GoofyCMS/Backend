using System.Collections.Generic;
using System.Reflection;
using Goofy.Core.Components.Base;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Goofy.Core.Components
{
    public class GoofyComponentsAssembliesProvider : IComponentsAssembliesProvider
    {
        private const string ComponentDirectoryPrefixPattern = "Goofy.Component.*";
        private readonly IComponentsDirectoryPathProvider _componentsPathProvider;
        private readonly string ComponentExtension = ".dll";
        /* Esta propiedad va a ser 'static' hasta que se busque una forma de resolver
           las dependencias del IServiceCollection de buena forma, porque esta clase 
           se registra como Singleton pero las instancia de IServiceCollection no 
           la resulve de buena forma.
        */
        private static List<Assembly> _componentsAssemblies;
        private readonly IAssemblyLoaderContainer _assemblyLoaderContainer;
        private readonly IAssemblyLoadContextAccessor _assemblyLoadContextAccessor;
        private readonly ILogger<GoofyComponentsAssembliesProvider> _logger;

        public GoofyComponentsAssembliesProvider(IComponentsDirectoryPathProvider componentsPathProvider,
                                                 IAssemblyLoaderContainer assemblyLoaderContainer,
                                                 IAssemblyLoadContextAccessor assemblyLoadContextAccessor,
                                                 ILogger<GoofyComponentsAssembliesProvider> logger
                                                 )
        {
            _componentsPathProvider = componentsPathProvider;
            _assemblyLoaderContainer = assemblyLoaderContainer;
            _assemblyLoadContextAccessor = assemblyLoadContextAccessor;
            _logger = logger;
        }

        public IEnumerable<Assembly> ComponentsAssemblies
        {
            get
            {
                // TODO Ver por qué _componentsAssemblies == null evalúa 2 veces true
                if (_componentsAssemblies == null)
                {
                    LoadAssemblies();
                    // return Enumerable.Empty<Assembly>();
                }
                return _componentsAssemblies;
            }
        }

        public void LoadAssemblies()
        {
            _componentsAssemblies = new List<Assembly>();
            var componentsDirectoryPath = _componentsPathProvider.GetComponentsDirectoryPath();

            var assemblyLoadContext = _assemblyLoadContextAccessor.Default;
            using (_assemblyLoaderContainer.AddLoader(new GoofyPlatformAbstractionsAssemblyLoader(assemblyLoadContext)))
            {
                foreach (var componentDirectory in Directory.EnumerateDirectories(componentsDirectoryPath).Select(dir => new DirectoryInfo(dir)))
                {
                    var componentName = componentDirectory.Name;

                    //Si la carpeta contiene un project.json, se va a asumir que es un dependencia de tipo proyecto
                    //var projectJsonPath = Path.Combine(componentDirectory.FullName, "project.json");
                    //var dllPath = Path.Combine(componentDirectory.FullName, string.Format("{0}.dll", componentName));
                    //if (File.Exists(projectJsonPath))
                    //{
                    //    _componentsAssemblies.Add(assemblyLoadContext.Load(projectJsonPath));
                    //}
                    var componentDllPath = Path.Combine(componentDirectory.FullName, string.Format("{0}{1}", componentName, ComponentExtension));
                    if (File.Exists(componentDllPath))
                    {
                        _componentsAssemblies.Add(assemblyLoadContext.LoadFile(componentDllPath));
                        _logger.LogInformation("La Componente \"{0}\", fue cargada satisfactoriamente.", componentName);
                    }   
                    else
                    {
                        _logger.LogWarning("La carpeta \"{0}\" situada en \"{1}\" no posee no es un Component válida.", componentName, componentDirectory.FullName);
                    }
                }
            }

            //foreach (var componentPath in Directory.GetDirectories(componentsDirectoryPath, ComponentDirectoryPrefixPattern, SearchOption.TopDirectoryOnly))
            //{
            //    var dllPath = string.Format("{0}\\{1}{2}", componentPath, new DirectoryInfo(componentPath).Name, ComponentExtension);
            //    Assembly currentAssembly;
            //    AssemblyName assemblyName;
            //    try
            //    {
            //        assemblyName = AssemblyName.GetAssemblyName(dllPath);
            //        currentAssembly = Assembly.Load(assemblyName);//cambiar esta línea por appDomain.Load(assemblyName) en caso de que se quiera cargar el assembly al dominio vigente
            //        _componentsAssemblies.Add(currentAssembly);
            //    }
            //    catch (TargetInvocationException e)
            //    {
            //        //TODO:agregar a los logs ComponenteInválida porque no presenta una dll.
            //        continue;
            //    }
            //}
        }
    }
}
