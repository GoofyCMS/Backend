using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using Goofy.Core.Components;
using Goofy.Core.Infrastructure;
using Goofy.Data;
using Goofy.Data.Context.Extensions;
using Microsoft.Data.Entity;
using System.Reflection;

namespace Goofy.WebFramework.Components
{
    public class GoofyWebComponentManager : GoofyComponentManager
    {
        private readonly IDependencyContainer _container;

        public GoofyWebComponentManager(IDependencyContainer container)
            : base()
        {
            _container = container;
        }

        public override IEnumerable<Component> GetComponents()
        {
            using (var compContext = _container.Resolve<ComponentContext>())
            {
                return compContext.Components.ToArray();
            }
        }

        public virtual void LoadComponentsFromAssemblies(IDependencyContainer dependencyContainer)
        {
            using (var compContext = _container.Resolve<ComponentContext>())
            {
                compContext.CreateTablesIfNotExists(dependencyContainer);
            }

            var allComponents = GetComponents();
            IEnumerable<Component> installedComponents;

            //Explorar todas las componentes que no están en la base de datos.
            using (var componentContext = _container.Resolve<ComponentContext>())
            {
                foreach (var compAttrsInfo in ComponentsAttributes)
                {
                    if (allComponents.Where(c => c.Name == compAttrsInfo.FullName).Count() == 0)//Se encontró una componente que no está en la base de datos
                    {
                        var isSystemComponent = false;
                        try
                        {
                            var compConfig = ComponentsConfig[compAttrsInfo.FullName];
                            isSystemComponent = compConfig.CompConfig.IsSystemPlugin;
                        }
                        catch { }
                        componentContext.Components.Add(
                                new Component
                                {
                                    Name = compAttrsInfo.FullName,
                                    Installed = isSystemComponent,
                                    Version = compAttrsInfo.Version.ToString(),
                                    IsSystemComponent = isSystemComponent
                                }
                            );
                    }
                }
                installedComponents = componentContext.Components.Where(c => c.Installed).ToArray();
                componentContext.SaveChanges();
            }

            //Asegurar que las tablas de las componentes instaladas estén creadas.
            foreach (var component in installedComponents)
            {
                var componentAssembly = ComponentAssemblies.Where(comp => comp.FullName == component.Name).First();
                InstallComponentTablesFromAssembly(componentAssembly, dependencyContainer);
            }
        }

        private void InstallComponentTablesFromAssembly(Assembly componentAssembly, IDependencyContainer dependencyContainer)
        {
            Type contextObjectType = componentAssembly.FindObjectContext();
            if (contextObjectType != null)
            {
                using (var contextObject = (DbContext)Activator.CreateInstance(contextObjectType))
                {
                    contextObject.CreateTablesIfNotExists(dependencyContainer);
                }
            }
        }

        public override string GetComponentsDirectoryPath()
        {
            var wwwDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            var componentsDirectoryPath = string.Format("{0}\\{1}", wwwDirectory.Parent.FullName, ComponentDirectoryName);
            return componentsDirectoryPath;
        }
    }
}
