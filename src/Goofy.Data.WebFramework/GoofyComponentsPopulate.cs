using System;
using System.Linq;

using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Migrations.Internal;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Extensions.DependencyInjection;

using Goofy.Core.Infrastructure;
using Goofy.Core.Components;
using Goofy.Core.Components.Base;
using Goofy.Core.Components.Configuration;
using Goofy.Core.Components.Extensions;

using Goofy.Data.DataProvider;
using Goofy.Extensions;

namespace Goofy.Data.WebFramework.Components
{
    /// <summary>
    /// Esta clase tiene como objetivo sincronizar la base de datos con las componentes
    /// que se encuentren instaladas en el sistema. Si la componente está instalada sus
    /// tablas serán creadas, en caso contrario serán eliminadas.
    /// </summary>
    public class GoofyComponentsPopulate : IRunAtStartup
    {
        private readonly IServiceCollection _services;
        private readonly IComponentStateManager _componentStateManager;
        private readonly IComponentStore _componentStore;
        private readonly IComponentsInfoProvider _componentsInfoProvider;
        private readonly IComponentsAssembliesProvider _componentAssembliesProvider;
        private readonly MigrationsModelDiffer _modelDiffer;
        private readonly MigrationsSqlGenerator _sqlGenerator;
        private readonly IEntityFrameworkDataProvider _dataProvider;

        public int Order
        {
            get
            {
                /*
                Asegurar que esta tarea de inicio se corra de primera
                al iniciar el framework
                */
                return -1000;
            }
        }

        public GoofyComponentsPopulate(
                                       IServiceCollection services,
                                       IComponentStateManager componentStateManager,
                                       IComponentStore componentStore,
                                       IComponentsInfoProvider componentsInfoProvider,
                                       IComponentsAssembliesProvider componentsAssembliesProvider,
                                       MigrationsModelDiffer modelDiffer,
                                       MigrationsSqlGenerator sqlGenerator,
                                       IEntityFrameworkDataProvider dataProvider
                                      )
        {
            _services = services;
            _componentStateManager = componentStateManager;
            _componentStore = componentStore;
            _componentsInfoProvider = componentsInfoProvider;
            _componentAssembliesProvider = componentsAssembliesProvider;
            _modelDiffer = modelDiffer;
            _sqlGenerator = sqlGenerator;
            _dataProvider = dataProvider;
        }

        public void Run()
        {
            _componentStore.StartStore(_services);

            var allComponents = _componentStore.Components;

            foreach (var compInfo in _componentsInfoProvider.ComponentsInfo)
            {
                if (allComponents.Where(c => c.FullName == compInfo.FullName).Count() == 0)//Se encontró una componente que no está en la base de datos
                {
                    var isSystemComponent = false;
                    ///That suck, 
                    ///TODO: Make this code logic be done by ComponentInfo Improve this piece of code
                    var assembly = _componentAssembliesProvider.ComponentsAssemblies.Where(a => a.FullName == compInfo.FullName).First();
                    var configType = assembly.FindExportedObject<ComponentConfig>();
                    if (configType != null)
                    {
                        var compConfig = (ComponentConfig)_services.GetConfiguration(configType);
                        isSystemComponent = compConfig.CompConfig.IsSystemPlugin;
                    }

                    var newComponent = new Component
                    {
                        FullName = compInfo.FullName,
                        Installed = isSystemComponent,
                        Version = compInfo.Version.ToString(),
                        IsSystemComponent = isSystemComponent
                    };
                    _componentStore.AddComponent(newComponent);
                }
            }
            /*
                Asegurar que las tablas de las componentes instaladas estén creadas,
                y las de las componentes no instaladas no lo estén.
            */
            foreach (var c in _componentStore.Components)
            {
                var componentAssembly = _componentAssembliesProvider.ComponentsAssemblies.Where(comp => comp.FullName == c.FullName).First();
                Type contextObjectType = componentAssembly.FindExportedObject<DbContext>();
                if (contextObjectType != null)
                {
                    var contextObject = (DbContext)_services.Resolve(contextObjectType);
                    if (c.Installed)
                        contextObject.CreateTablesIfNotExists(_modelDiffer, _sqlGenerator, _dataProvider);
                    else
                        contextObject.DropTables(_modelDiffer, _sqlGenerator, true);
                }
            }
        }
    }
}
