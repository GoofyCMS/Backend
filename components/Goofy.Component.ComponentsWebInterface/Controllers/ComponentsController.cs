using System.Linq;

using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Migrations.Internal;
using Microsoft.Data.Entity.Migrations;

using Goofy.Core.Components.Base;
using Goofy.Data;
using Goofy.Data.Components;
using Goofy.Data.DataProvider;
using Microsoft.Extensions.Logging;

namespace Goofy.Component.ComponentsWebInterface.Controllers
{
    [Route("components")]
    public class ComponentsController : Controller
    {
        private readonly ComponentContext _componentContext;
        private readonly IComponentsAssembliesProvider _componentsAssembliesProvider;
        private readonly IEntityFrameworkDataProvider _dataProvider;
        private readonly ILogger _logger;
        private readonly MigrationsModelDiffer _modelDiffer;
        private readonly MigrationsSqlGenerator _sqlGenerator;

        public ComponentsController(IComponentsAssembliesProvider componentsAssembliesProvider,
                                    ComponentContext componentContext,
                                    MigrationsModelDiffer modelDiffer,
                                    MigrationsSqlGenerator sqlGenerator,
                                    IEntityFrameworkDataProvider dataProvider,
                                    ILogger<ComponentsController> logger
                                    )
        {
            _componentsAssembliesProvider = componentsAssembliesProvider;
            _componentContext = componentContext;
            _modelDiffer = modelDiffer;
            _sqlGenerator = sqlGenerator;
            _dataProvider = dataProvider;
            _logger = logger;
        }

        [HttpGet("list")]
        public IActionResult Components()
        {
            return new ObjectResult(_componentContext.Components.Where(c => !c.IsSystemComponent).ToArray());
        }

        [HttpGet("install/{id:int}")]
        public IActionResult Install(int id)
        {
            var objectContext = LoadObjectContextFromComponentId(id);
            if (objectContext != null)
                objectContext.CreateTablesIfNotExists(_modelDiffer, _sqlGenerator, _dataProvider);

            SetComponentInstalledState(id, true);
            return new HttpOkResult();
        }

        [HttpGet("uninstall/{id:int}")]
        public IActionResult Uninstall(int id)
        {
            var objectContext = LoadObjectContextFromComponentId(id);
            if (objectContext != null)
                objectContext.DropTables(_modelDiffer, _sqlGenerator);

            SetComponentInstalledState(id, false);
            return new HttpOkResult();
        }


        //TODO: Ver si este método no puede ser agregado como útil de otra librería.
        private DbContext LoadObjectContextFromComponentId(int id)
        {
            var component = _componentContext.Components.FirstOrDefault(c => (c.ComponentId == id) && (!c.IsSystemComponent));
            if (component == null)
                return null;
            var componentAssembly = _componentsAssembliesProvider.ComponentsAssemblies.Where(ass => ass.GetName().FullName == component.FullName).First();
            var contextObject = componentAssembly.FindExportedObject<DbContext>();
            if (contextObject != null)
            {
                var objectContext = (DbContext)HttpContext.ApplicationServices.GetService(contextObject);
                return objectContext;
            }
            return null;
        }

        private void SetComponentInstalledState(int id, bool installed)
        {
            var component = _componentContext.Components.ToArray().FirstOrDefault(c => (c.ComponentId == id) && (!c.IsSystemComponent));
            component.Installed = installed;
            _componentContext.SaveChanges();
        }
    }
}
