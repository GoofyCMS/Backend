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

using Goofy.Data.DataProvider;
using Goofy.Data.WebFramework.Components;

using Goofy.Extensions;

namespace Goofy.Data.WebFramework
{
    /// <summary>
    /// Esta clase tiene como objetivo sincronizar la base de datos con las componentes
    /// que se encuentren instaladas en el sistema. Si la componente está instalada sus
    /// tablas serán creadas si no lo están, en caso contrario serán eliminadas.
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
        private readonly ComponentConfig _componentConfiguration;

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
                                       IComponentsAssembliesProvider componentsAssembliesProvider,
                                       IOptions<ComponentConfig> componentConfigOptions
                                      )
        {
            _services = services;
            _compContext = compContext;
            _modelDiffer = modelDiffer;
            _sqlGenerator = sqlGenerator;
            _dataProvider = dataProvider;
            _componentsInfoProvider = componentsInfoProvider;
            _componentsAssembliesProvider = componentsAssembliesProvider;
            _componentConfiguration = componentConfigOptions.Value;
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
                    try
                    {
                        ///That suck, TODO: Improve this piece of code
                        var assembly = _componentsAssembliesProvider.ComponentsAssemblies.Where(a => a.FullName == compInfo.FullName).First();
                        var configType = assembly.FindExportedObject<ComponentConfig>();
                        var compConfig = (ComponentConfig)Activator.CreateInstance(configType);
                    }
                    catch
                    {

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
                UpdateComponentTablesFromAssembly(componentAssembly, _services, component.Installed);
            }
        }

        private void UpdateComponentTablesFromAssembly(Assembly componentAssembly, IServiceCollection services, bool installed)
        {
            Type contextObjectType = componentAssembly.FindExportedObject<DbContext>();
            if (contextObjectType != null)
            {
                var contextObject = (DbContext)services.Resolve(contextObjectType);
                if (installed)
                    contextObject.CreateTablesIfNotExists(_modelDiffer, _sqlGenerator, _dataProvider);
                else
                    contextObject.DropTables(_modelDiffer, _sqlGenerator, true);
            }
        }
    }
}
