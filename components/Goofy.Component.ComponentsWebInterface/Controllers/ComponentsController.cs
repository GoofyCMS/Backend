using System.Linq;

using Microsoft.AspNet.Mvc;

using Goofy.Core.Components;
using Goofy.Data;
using Goofy.WebFramework.Components;
using Goofy.WebFramework;
using Goofy.Core.Infrastructure;

using Goofy.Data.Context.Extensions;
using Microsoft.Data.Entity;

namespace Goofy.Component.ComponentsWebInterface.Controllers
{
    [Route("components")]
    public class ComponentsController : Controller
    {
        private readonly ComponentContext _componentContext;
        private readonly GoofyComponentManager _componentManager;

        public ComponentsController(GoofyComponentManager componentManager, ComponentContext componentContext)
        {
            _componentContext = componentContext;
            _componentManager = componentManager;
        }

        [HttpGet("list")]
        public IActionResult Components()
        {
            return new ObjectResult(_componentContext.Components.ToArray());
        }

        [HttpGet("install/{id:int}")]
        public IActionResult Install(int id)
        {
            var objectContext = GetObjectContext(id);
            if (objectContext == null)
                return new HttpNotFoundResult();

            var dependencyContainer = (IDependencyContainer)HttpContext.ApplicationServices.GetService(typeof(IDependencyContainer));
            objectContext.CreateTablesIfNotExists(dependencyContainer);
            return new HttpOkResult();
        }

        [HttpGet("uninstall/{id:int}")]
        public IActionResult Uninstall(int id)
        {
            var objectContext = GetObjectContext(id);
            if (objectContext == null)
                return new HttpNotFoundResult();

            var dependencyContainer = (IDependencyContainer)HttpContext.ApplicationServices.GetService(typeof(IDependencyContainer));
            objectContext.DropTables(dependencyContainer);
            return new HttpOkResult();
        }


        //TODO: Ver si este método no puede ser agregado como útil de otra librería.
        private DbContext GetObjectContext(int id)
        {
            var component = _componentContext.Components.FirstOrDefault(c => c.ComponentId == id);
            if (component == null)
                return null;

            var componentAssembly = _componentManager.ComponentAssemblies.Where(ass => ass.GetName().Name == component.Name).First();
            var contextObject = componentAssembly.FindObjectContext();
            var objectContext = (DbContext)HttpContext.ApplicationServices.GetService(contextObject);
            return objectContext;
        }
    }
}
