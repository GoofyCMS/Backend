using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Goofy.Core.Components.Base;

namespace Goofy.Component.ComponentsWebInterface.Controllers
{
    [Route("components")]
    public class ComponentsController : Controller
    {
        private readonly IComponentStateManager _componentStateManager;

        public ComponentsController(IComponentStateManager componentStateManager)
        {
            _componentStateManager = componentStateManager;
        }

        [HttpGet("list")]
        public IActionResult Components()
        {
            return new ObjectResult(_componentStateManager.ComponentStore.Components.Where(c => !c.IsSystemComponent));
        }

        [HttpGet("install/{id:int}")]
        public IActionResult Install(int id)
        {
            try
            {
                _componentStateManager.InstallComponentById(id);
            }
            catch (Exception e)
            {
                if (e is ArgumentException || e is InvalidOperationException)
                    return new HttpNotFoundResult();
                else
                    return new ObjectResult(e.Message);
            }

            return new HttpOkResult();
        }

        [HttpGet("uninstall/{id:int}")]
        public IActionResult Uninstall(int id)
        {
            try
            {
                _componentStateManager.UninstallComponentById(id);
            }
            catch (Exception e)
            {
                if (e is ArgumentException || e is InvalidOperationException)
                    return new HttpNotFoundResult();
                else
                    return new ObjectResult(e.Message);
            }
            return new HttpOkResult();
        }
    }
}
