using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Goofy.Core.Components.Configuration;
using Goofy.Configuration.Extensions;

namespace Goofy.Core.Components
{
    public abstract class GoofyComponentManager : IComponentManager
    {
        #region Clase de Ayuda

        protected class ComponentAttributes
        {
            //private readonly List<ComponentAttributes> _referencedAssemblies;

            public ComponentAttributes(string fullName, Version version, string path)
            {
                FullName = fullName;
                Version = version;
                Path = path;
                //_referencedAssemblies = new List<ComponentAttributes>();
            }
            public string FullName { get; private set; }
            public Version Version { get; private set; }
            public string Path { get; private set; }

            //public IEnumerable<ComponentAttributes> ReferencedAssemblies { get { return _referencedAssemblies; } }

            //public void AddReference(ComponentAttributes componentAttributes)
            //{
            //    _referencedAssemblies.Add(componentAttributes);
            //}
            //public void AddMultipleReferences(IEnumerable<ComponentAttributes> componentAttributes)
            //{
            //    _referencedAssemblies.AddRange(componentAttributes);
            //}
        }

        #endregion


        #region Campos

        private const string ComponentDirectoryPrefixPattern = "Goofy.Component.*";
        //private const string ComponentDirectoryPrefixPattern = "*";
        protected readonly string ComponentDescriptionFileName = "config.json";
        protected readonly string ComponentExtension = ".dll";
        protected readonly string ComponentDirectoryName = "components";

        #endregion


        #region Constructor

        public GoofyComponentManager()
        {
            LoadComponentsAssemblies();
            ComponentsAttributes = GetComponentsAttributes();
        }

        #endregion


        #region Properties

        public virtual IEnumerable<Assembly> ComponentAssemblies { get; private set; }

        public virtual IEnumerable<string> ValidComponentsJson { get; private set; }

        public virtual Dictionary<string, ComponentConfig> ComponentsConfig { get; private set; }

        protected virtual Dictionary<string, List<Assembly>> ExtraDependencies { get; private set; }

        protected virtual IEnumerable<ComponentAttributes> ComponentsAttributes { get; private set; }


        #endregion


        #region Métodos

        /*
            Este método es la primera parte para comenzar el chequeo de dependencias que se va a realizar
            sobre las componentes de 3ros que se integren al sistema.
            Chequeo de dependencias: 
                Si es una dependencia que no es una componente de Goofy, debe proveer la dll
                En caso contrario, el framework va a chequear que esté en la carpeta de componentes(en principio 
                no se ha considerado el chequeo de versión además)
        */
        protected List<ComponentAttributes> GetComponentsAttributes()
        {
            var componentAttributes = new Dictionary<string, ComponentAttributes>();

            //Crear la clase ComponentAttributes que representa a esta componente.
            foreach (var c in ComponentAssemblies)
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

        protected virtual void LoadComponentsAssemblies()
        {
            var validComponentsJson = new List<string>();
            var componentsDirectoryPath = GetComponentsDirectoryPath();
            var componentsAssemblies = new List<Assembly>();
            var componentsConfig = new Dictionary<string, ComponentConfig>();
            var appDomain = AppDomain.CurrentDomain;
            var extraDependencies = new Dictionary<string, List<Assembly>>();

            foreach (var componentPath in Directory.GetDirectories(componentsDirectoryPath, ComponentDirectoryPrefixPattern, SearchOption.TopDirectoryOnly))
            {
                /* 
                    Convenciones sobre configuraciones: La componente se va a encontrar dentro de la carpeta "components",
                    y va a estar formado por una carpeta  que contendrá una dll con el mismo nombre de la carpeta que contiene 
                    el código de la componente. Esta puede incluir controladores, rutas, tareas de inicio, tareas de registro 
                    para el contenedor de dependencias.
                */
                var dllPath = string.Format("{0}\\{1}{2}", componentPath, new DirectoryInfo(componentPath).Name, ComponentExtension);
                Assembly currentAssembly;
                AssemblyName assemblyName;
                try
                {
                    assemblyName = AssemblyName.GetAssemblyName(dllPath);
                    currentAssembly = Assembly.Load(assemblyName);//cambiar esta línea por appDomain.Load(assemblyName) en caso de que se quiera cargar el assembly al dominio vigente
                    componentsAssemblies.Add(currentAssembly);
                }
                catch (TargetInvocationException e)
                {
                    //TODO:agregar a los logs ComponenteInválida porque no presenta una dll.
                    continue;
                }

                var componentJsonConfigFile = string.Format("{0}\\config.json", componentPath);
                if (ValidConfigFile(componentPath, componentJsonConfigFile))
                {
                    validComponentsJson.Add(componentJsonConfigFile);
                    var componentConfig = ConfigurationExtensions.GetConfiguration<ComponentConfig>(componentJsonConfigFile, assemblyName.Name);
                    componentsConfig.Add(currentAssembly.FullName, componentConfig);
                }
                else
                {
                    //TODO:Añadir mensaje de warning a los logs
                }

                var searchPattern = "*.dll";
                List<Assembly> depAssemblies = new List<Assembly>();
                foreach (var extraDll in Directory.GetFiles(componentPath, searchPattern))
                {
                    if (extraDll != dllPath)
                    {
                        depAssemblies.Add(Assembly.Load(extraDll));
                    }
                }
                extraDependencies.Add(currentAssembly.FullName, depAssemblies);

            }
            ValidComponentsJson = validComponentsJson;
            ComponentAssemblies = componentsAssemblies;
            ComponentsConfig = componentsConfig;
            ExtraDependencies = extraDependencies;
        }

        public virtual string GetComponentsDirectoryPath()
        {
            return Directory.GetCurrentDirectory();
        }

        protected virtual bool ValidConfigFile(string componentPath, string componentJsonConfigFile)
        {
            //Extraer el nombre de la componente
            var fileInfo = new FileInfo(componentPath);
            var componentName = fileInfo.Name;
            JObject jsonObject;
            try
            {
                jsonObject = JObject.Parse(File.ReadAllText(componentJsonConfigFile));
            }
            catch
            {
                return false;
            }
            JToken token;
            if (jsonObject.Count != 1 || !jsonObject.TryGetValue(componentName, out token))
                return false;
            return true;
        }

        public abstract IEnumerable<Component> GetComponents();

        public virtual IEnumerable<Component> GetInstalledComponents()
        {
            return GetComponents().Where(c => c.Installed);
        }

        public virtual IEnumerable<Component> GetNonInstalledComponents()
        {
            return GetComponents().Where(c => !c.Installed);
        }

        #endregion


        #region Métodos de ayuda

        private bool IsGoofyComponent(string dllName)
        {
            return Regex.IsMatch(dllName, ComponentDirectoryPrefixPattern);
        }

        private ComponentAttributes GetComponentAttributesFromAssembly(Assembly assembly)
        {
            var assemblyName = assembly.GetName();
            return new ComponentAttributes(assemblyName.FullName, assemblyName.Version, assembly.Location);
        }

        #endregion
    }
}
