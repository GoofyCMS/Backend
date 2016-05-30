using Goofy.Domain.Plugins.Entity;
using Goofy.Application.PluggableCore.DTO;
using Goofy.Presentation.Core.Controllers;
using Microsoft.AspNet.Mvc;
using Goofy.Application.PluggableCore.Services;
using Goofy.Presentation.PluggableCore.Providers;

namespace Goofy.Presentation.PluggableCore.Controllers
{
    [Route("plugins")]
    public class PluginsReadOnlyController : BaseReadOnlyController<Plugin, PluginItem, int>
    {
        public PluginsReadOnlyController(PluginServiceMapper<Plugin, PluginItem> service, PluginContextProvider provider)
            : base(service, provider)
        {
        }

        /* con objetivos de testing*/
        [Route("test")]
        public string PluginReadOnlyController()
        {
            return "Hello from PluginsReadOnlyController";
        }
    }
}
