using System;
using System.Linq;

using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Migrations.Internal;
using Microsoft.Data.Entity.Migrations;

using Goofy.Core.Components;
using Goofy.Core.Components.Base;
using Goofy.Core.Components.Exceptions;

using Goofy.Data;
using Goofy.Data.DataProvider;

namespace Goofy.Core.WebFramework.Components
{
    public class ComponentStateManager : IComponentStateManager
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IComponentStore _componentStore;
        private readonly IComponentsAssembliesProvider _componentsAssembliesProvider;
        private readonly MigrationsModelDiffer _modelDiffer;
        private readonly MigrationsSqlGenerator _sqlGenerator;
        private readonly IEntityFrameworkDataProvider _dataProvider;

        //TODO: Testear que se resuelva el IServiceProvider
        public ComponentStateManager(
                                     IServiceProvider serviceProvider,
                                     IComponentStore componentStore,
                                     IComponentsAssembliesProvider componentsAssembliesProvider,
                                     MigrationsModelDiffer modelDiffer,
                                     MigrationsSqlGenerator sqlGenerator,
                                     IEntityFrameworkDataProvider dataProvider)
        {
            _serviceProvider = serviceProvider;
            _componentStore = componentStore;
            _componentsAssembliesProvider = componentsAssembliesProvider;
            _modelDiffer = modelDiffer;
            _sqlGenerator = sqlGenerator;
            _dataProvider = dataProvider;
        }

        public IComponentStore ComponentStore
        {
            get
            {
                return _componentStore;
            }
        }

        public void InstallComponentById(int id)
        {
            var component = _componentStore.Components.Where(c => c.ComponentId == id).FirstOrDefault();
            if (component == null)
            {
                throw new ArgumentException(string.Format("Goofy can not find a component with ComponentId =\"{0}\"", id));
            }
            if (component.Installed)
            {
                throw new ComponentOperationException(component, true);
            }
            if (component.IsSystemComponent)
            {
                throw new InvalidOperationException("Can not uninstall a system component.");
            }
            var objectContext = GetComponentAssembly(component);
            if (objectContext != null)
            {
                objectContext.CreateTablesIfNotExists(_modelDiffer, _sqlGenerator, _dataProvider);
            }

            component.Installed = true;
            _componentStore.UpdateComponent(component);
        }

        public void UninstallComponentById(int id)
        {
            var component = _componentStore.Components.Where(c => c.ComponentId == id).FirstOrDefault();
            if (component == null)
            {
                throw new ArgumentException(string.Format("Goofy can not find a component with ComponentId =\"{0}\"", id));
            }
            if (!component.Installed)
            {
                throw new ComponentOperationException(component, false);
            }
            if (component.IsSystemComponent)
            {
                throw new InvalidOperationException("Can not uninstall a system component.");
            }
            var objectContext = GetComponentAssembly(component);
            if (objectContext != null)
                objectContext.DropTables(_modelDiffer, _sqlGenerator, true);

            component.Installed = false;
            _componentStore.UpdateComponent(component);
        }

        private DbContext GetComponentAssembly(Component component)
        {
            var componentAssembly = _componentsAssembliesProvider.ComponentsAssemblies.Where(ass => ass.GetName().FullName == component.FullName).First();
            var contextObject = componentAssembly.FindExportedObject<DbContext>();
            if (contextObject != null)
            {
                var objectContext = (DbContext)_serviceProvider.GetService(contextObject);
                return objectContext;
            }
            return null;
        }
    }
}
