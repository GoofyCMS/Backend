using System;
using System.Reflection;
using System.Linq;

using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;

using Goofy.Core.Infrastructure;
using Goofy.Core.Components;
using Goofy.Core.Components.Base;
using Goofy.Core.Components.Configuration;

using Goofy.Data.Components;
using Goofy.Data.DataProvider;

using Goofy.Extensions;

namespace Goofy.Data.WebFramework
{
    /// <summary>
    /// Esta clase tiene como objetivo sincronizar la base de datos con las componentes
    /// que se encuentren instaladas en el sistema. Si la componente está instalada sus
    /// tablas serán creadas, en caso contrario serán eliminadas.
    /// </summary>
    public class GoofyComponentsPopulate : IRunAtStartup
    {
        private readonly IServiceCollection _services;
        private readonly ComponentContext _compContext;
        private readonly IEntityFrameworkDataProvider _dataProvider;
        private readonly IMigrationsModelDiffer _modelDiffer;
        private readonly MigrationsSqlGenerator _sqlGenerator;
        private readonly IComponentsInfoProvider _componentsInfoProvider;
        private readonly IComponentsAssembliesProvider _componentsAssembliesProvider;

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
                                       ComponentContext compContext,
                                       IMigrationsModelDiffer modelDiffer,
                                       IComponentsInfoProvider componentsInfoProvider,
                                       MigrationsSqlGenerator sqlGenerator,
                                       IEntityFrameworkDataProvider dataProvider,
                                       IComponentsAssembliesProvider componentsAssembliesProvider
                                      )
        {
            _services = services;
            _compContext = compContext;
            _modelDiffer = modelDiffer;
            _sqlGenerator = sqlGenerator;
            _dataProvider = dataProvider;
            _componentsInfoProvider = componentsInfoProvider;
            _componentsAssembliesProvider = componentsAssembliesProvider;
        }

        public void Run()
        {
            _compContext.CreateTablesIfNotExists(_modelDiffer, _sqlGenerator, _dataProvider);

            var allComponents = _compContext.Components.ToArray();
            //IEnumerable<Component> installedComponents;

            foreach (var compInfo in _componentsInfoProvider.ComponentsInfo)
            {
                if (allComponents.Where(c => c.FullName == compInfo.FullName).Count() == 0)//Se encontró una componente que no está en la base de datos
                {
                    var isSystemComponent = false;

                    ///That suck, 
                    ///TODO: Make this code logic be done by ComponentInfo Improve this piece of code
                    var assembly = _componentsAssembliesProvider.ComponentsAssemblies.Where(a => a.FullName == compInfo.FullName).First();
                    var configType = assembly.FindExportedObject<ComponentConfig>();
                    if (configType != null)
                    {
                        var compConfig = (ComponentConfig)_services.GetConfiguration(configType);
                        isSystemComponent = compConfig.CompConfig.IsSystemPlugin;
                    }
                    _compContext.Components.Add(
                            new Component
                            {
                                FullName = compInfo.FullName,
                                Installed = isSystemComponent,
                                Version = compInfo.Version.ToString(),
                                IsSystemComponent = isSystemComponent
                            }
                        );
                }
            }
            //installedComponents = _compContext.Components.Where(c => c.Installed).ToArray();
            _compContext.SaveChanges();

            /*
                Asegurar que las tablas de las componentes instaladas estén creadas,
                y las de las componentes no instaladas no lo estén.
            */
            foreach (var component in _compContext.Components.ToArray())
            {
                var componentAssembly = _componentsAssembliesProvider.ComponentsAssemblies.Where(comp => comp.FullName == component.FullName).First();
                bool createTables = component.Installed || component.IsSystemComponent;
                UpdateComponentTablesFromAssembly(componentAssembly, createTables);
            }
        }

        private void UpdateComponentTablesFromAssembly(Assembly componentAssembly, bool createTables)
        {
            Type contextObjectType = componentAssembly.FindExportedObject<DbContext>();
            if (contextObjectType != null)
            {
                var contextObject = (DbContext)_services.Resolve(contextObjectType);
                if (createTables)
                    contextObject.CreateTablesIfNotExists(_modelDiffer, _sqlGenerator, _dataProvider);
                else
                    contextObject.DropTables(_modelDiffer, _sqlGenerator, true);
            }
        }
    }
}
