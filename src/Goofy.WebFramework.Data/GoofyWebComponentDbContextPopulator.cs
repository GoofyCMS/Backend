using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Extensions.DependencyInjection;

using Goofy.Core.Components;
using Goofy.Core.Components.Base;
using Goofy.Core.Components.Configuration;
using Goofy.Core.DependencyInjection.DesignTimeExtensions;

using Goofy.Data.DataProvider;
using Goofy.Data.Context.Extensions;

using Goofy.WebFramework.Data.Components;
using Goofy.WebFramework.Data.Services;

using Goofy.Configuration.Extensions;

namespace Goofy.WebFramework.Data
{
    internal class GoofyWebComponentDbContextPopulator : IComponentDbContextPopulator
    {
        private readonly IEntityFrameworkDataProvider _dataProvider;
        private readonly ComponentContext _compContext;
        private readonly IMigrationsModelDiffer _modelDiffer;
        private readonly MigrationsSqlGenerator _sqlGenerator;
        private readonly IComponentsInfoProvider _componentsInfoProvider;
        private readonly IComponentsAssembliesProvider _componentsAssembliesProvider;

        public GoofyWebComponentDbContextPopulator(ComponentContext compContext,
                                                   IMigrationsModelDiffer modelDiffer,
                                                   IComponentsInfoProvider componentsInfoProvider,
                                                   MigrationsSqlGenerator sqlGenerator,
                                                   IEntityFrameworkDataProvider dataProvider,
                                                   IComponentsAssembliesProvider componentsAssembliesProvider)
        {
            _compContext = compContext;
            _modelDiffer = modelDiffer;
            _sqlGenerator = sqlGenerator;
            _dataProvider = dataProvider;
            _componentsInfoProvider = componentsInfoProvider;
            _componentsAssembliesProvider = componentsAssembliesProvider;
        }

        public void PopulateComponentDbContext(IServiceCollection services)
        {
            _compContext.CreateTablesIfNotExists(_modelDiffer, _sqlGenerator, _dataProvider);

            var allComponents = _compContext.Components.ToArray();
            //IEnumerable<Component> installedComponents;

            foreach (var compAttrsInfo in _componentsInfoProvider.ComponentsInfo)
            {
                if (allComponents.Where(c => c.Name == compAttrsInfo.FullName).Count() == 0)//Se encontró una componente que no está en la base de datos
                {
                    var isSystemComponent = false;
                    try
                    {
                        var compInfo = _componentsInfoProvider.ComponentsInfo.Where(cI => cI.FullName == compAttrsInfo.FullName).First();
                        var compConfig = ConfigurationExtensions.GetConfiguration<ComponentConfig>(compInfo.ConfigFilePath, compInfo.Name);
                        isSystemComponent = compConfig.CompConfig.IsSystemPlugin;
                    }
                    catch
                    {

                    }
                    _compContext.Components.Add(
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
            //installedComponents = _compContext.Components.Where(c => c.Installed).ToArray();
            _compContext.SaveChanges();

            //Asegurar que las tablas de las componentes instaladas estén creadas.
            foreach (var component in _compContext.Components.ToArray())
            {
                var componentAssembly = _componentsAssembliesProvider.ComponentsAssemblies.Where(comp => comp.FullName == component.Name).First();
                UpdateComponentTablesFromAssembly(componentAssembly, services, component.Installed);
            }
        }

        private void UpdateComponentTablesFromAssembly(Assembly componentAssembly, IServiceCollection services, bool installed)
        {
            Type contextObjectType = componentAssembly.FindObjectContext();
            if (contextObjectType != null)
            {
                //Por ahora se necesita obligado un IServiceCollection porque la forma de instanciar el
                //tipo contextObjectType sin usar serviceCollection es mediante Activator.CreateInstance 
                //que usa el constructor vacío por defecto, por lo que no es capaz de resolver dependencias
                //using (var contextObject = (DbContext)Activator.CreateInstance(contextObjectType, ))
                //{
                //    contextObject.CreateTablesIfNotExists(_modelDiffer, _sqlGenerator, _dataProvider);
                //}
                var contextObject = (DbContext)services.Resolve(contextObjectType);
                if (installed)
                    contextObject.CreateTablesIfNotExists(_modelDiffer, _sqlGenerator, _dataProvider);
                else
                    contextObject.DropTables(_modelDiffer, _sqlGenerator, true);
            }
        }
    }
}
