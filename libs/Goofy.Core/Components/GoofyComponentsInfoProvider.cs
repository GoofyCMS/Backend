using System.Linq;
using System.Collections.Generic;
using System.Reflection;

using Goofy.Core.Components.Base;

namespace Goofy.Core.Components
{
    public class GoofyComponentsInfoProvider : IComponentsInfoProvider
    {
        private readonly IComponentsAssembliesProvider _componentAssembliesProvider;
        private readonly IComponentsConfigurationFileValidator _componentConfigFileValidator;

        private IEnumerable<ComponentInfo> _componentsInfo;

        public GoofyComponentsInfoProvider(IComponentsAssembliesProvider componentAssembliesProvider,
                                         IComponentsConfigurationFileValidator componentConfigFileValidator)
        {
            _componentAssembliesProvider = componentAssembliesProvider;
            _componentConfigFileValidator = componentConfigFileValidator;
        }

        public IEnumerable<ComponentInfo> ComponentsInfo
        {
            get
            {
                if (_componentsInfo == null)
                {
                    _componentsInfo = GetComponentsAttributes();
                }
                return _componentsInfo;
            }
        }

        /*
            Este método es la primera parte para comenzar el chequeo de dependencias que se va a realizar
            sobre las componentes de 3ros que se integren al sistema.
            Chequeo de dependencias: 
                Si es una dependencia que no es una componente de Goofy, debe proveer la dll
                En caso contrario, el framework va a chequear que esté en la carpeta de componentes(en principio 
                no se ha considerado el chequeo de versión además)
        */
        protected List<ComponentInfo> GetComponentsAttributes()
        {
            var componentAttributes = new Dictionary<string, ComponentInfo>();

            //Crear la clase ComponentAttributes que representa a esta componente.
            foreach (var c in _componentAssembliesProvider.ComponentsAssemblies)
            {
                var component = GetComponentAttributesFromAssembly(c);
                componentAttributes.Add(c.FullName, component);
            }

            //Inspección de dependencias, internas y externas.
            //foreach (var c in ComponentAssemblies)
            //{
            //    var goofyComponentReferences = c.GetReferencedAssemblies();

            //    foreach (var _ref in goofyComponentReferences)
            //    {
            //        if (IsGoofyComponent(_ref.Name))
            //        {
            //            //Tiene que estar presente en los ensamblados de las componentes

            //            if (ComponentAssemblies.Where(ass => ass.FullName == _ref.FullName).Count() == 0)
            //            //TODO: Asegurar también que la versión es válida
            //            {
            //                //Agregar a los logs que hay una dependencia de componente que no está en la carpeta de componentes
            //                throw new Exception("");
            //            }
            //        }
            //        else
            //        {
            //            //Puede ser una del sistema como mscorlib, System.*; o puede ser una externa que debería
            //            // estar presente en la carpeta components.

            //            if (AppDomain.CurrentDomain.GetAssemblies().Where(ass => ass.FullName == _ref.FullName).Count() == 0)
            //            //Verificar que no está cargada actualmente,(mscorlib, etc) TODO: hacer una pregunta más completa que garantice que el runtime puede satisfacer alguna dependencia.
            //            {
            //                //Asegurar que se encuentra dentro de las dependencias externas en la carpeta de la componente
            //                var externalAssemblies = ExtraDependencies[c.FullName];
            //                if (externalAssemblies.Where(ass => ass.FullName == _ref.FullName).Count() == 0)
            //                //TODO: Asegurar también que la versión es válida
            //                {
            //                    //Agregar a los logs que hay una dependencia externa que no se satisfizo
            //                    throw new Exception("");
            //                }
            //            }
            //        }
            //    }
            //}
            return componentAttributes.Values.ToList();
        }
        private ComponentInfo GetComponentAttributesFromAssembly(Assembly assembly)
        {
            var assemblyName = assembly.GetName();
            /*
                TODO
                    Arreglar el problema del Location
            */
            //var componentFolder = new FileInfo(assembly.Location).DirectoryName;
            var componentFolder = " ";
            var configFilePath = string.Format("{0}\\config.json", componentFolder);
            //string componentConfigFilePath = null;

            //if (_componentConfigFileValidator.IsValid(configFilePath, componentFolder))
            //    componentConfigFilePath = configFilePath;
            //else
            //{
            //    //agregar mensaje a los logs, hacer algo al respecto
            //}

            /*
                TODO:
                    Proveer el location que es no "" 
            */
            return new ComponentInfo(assemblyName.FullName, assemblyName.Version, "");
        }

    }
}
