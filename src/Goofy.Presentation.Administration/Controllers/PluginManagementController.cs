
using Goofy.Application.Administration;
using Goofy.Application.Administration.Services.Abstractions;
using Microsoft.AspNet.Mvc;

namespace Goofy.Presentation.Administration.Controllers
{
    [Route("api/administration/plugins")]
    public class PluginManagementController : Controller
    {
        private readonly IPluginManager _pluginManager;

        public PluginManagementController(IPluginManager pluginManager)
        {
            _pluginManager = pluginManager;
        }

        [HttpGet("enable/{id}")]
        public IActionResult Enable(int id)
        {
            return Map(_pluginManager.Enable(id));
        }

        [HttpGet("disable/{id}")]
        public IActionResult Disable(int id)
        {
            return Map(_pluginManager.Disable(id));
        }

        private IActionResult Map(PluginEnabledDisabledResult result)
        {
            switch (result)
            {
                case PluginEnabledDisabledResult.Ok: return new HttpOkResult();
                case PluginEnabledDisabledResult.NotFound: return new HttpNotFoundResult();
                case PluginEnabledDisabledResult.AlreadyEnabled: return new BadRequestObjectResult("Plugin already Enabled");
                case PluginEnabledDisabledResult.AlreadyDisabled: return new BadRequestObjectResult("Plugin already Disabled");
                default: return null;
            }
        }
    }
}
