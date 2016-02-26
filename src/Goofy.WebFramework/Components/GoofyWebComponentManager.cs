using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using Goofy.Core.Components;
using Goofy.Core.Infrastructure;
using Goofy.Data;
using Goofy.Data.Context.Extensions;
using Microsoft.Data.Entity;

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
            //Crear las tablas de las componentes del sistema


            var allComponents = GetComponents();
            var installedComponents = allComponents.Where(c => c.Installed);

            //Asegurar que las tablas de las componentes instaladas estén creadas.
            foreach (var component in installedComponents)
            {
                var componentAssembly = ComponentAssemblies.Where(comp => comp.GetName().Name == component.Name).First();
                Type contextObjectType = componentAssembly.FindObjectContext();
                if (contextObjectType == null)
                {
                    /* Esta condición en este caso no debería dar True nunca porque en principio sólo 
                    se le va a poder hacer install a las componentes que tengan algún GoofyObjectContext
                    */
                    continue;
                }
                using (var contextObject = (DbContext)Activator.CreateInstance(contextObjectType))
                {
                    contextObject.CreateTablesIfNotExists(dependencyContainer);
                }
            }

            //Ver si se subió alguna componente que no está en la base de datos
            using (var componentContext = _container.Resolve<ComponentContext>())
            {
                foreach (var compAttrsInfo in ComponentAttributesInfo)
                {
                    if (allComponents.Where(c => c.Name == compAttrsInfo.Name).Count() == 0)//Se encontró una componente que no está en la base de datos
                    {
                        componentContext.Components.Add(
                                new Component
                                {
                                    Name = compAttrsInfo.Name,
                                    Installed = false,
                                    Version = compAttrsInfo.Version.ToString()
                                }
                            );
                    }
                }
                componentContext.SaveChanges();
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
