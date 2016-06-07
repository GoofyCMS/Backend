using Goofy.Application.PluggableCore;
using Goofy.Application.PluggableCore.Abstractions;
using Goofy.Domain.PluggableCore.Entity;
using Goofy.Domain.PluggableCore.Service.Data;
using Microsoft.AspNet.Mvc;
using System;

namespace Goofy.Presentation.PluggableCore.Controllers
{
    [Route("api/plugins")]
    public class PluginsManagementController : Controller
    {
        private readonly IPluginManager _pluginManager;

        public PluginsManagementController(IPluginManager pluginManager)
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
