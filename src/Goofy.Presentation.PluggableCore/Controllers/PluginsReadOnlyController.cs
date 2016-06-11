using Goofy.Domain.PluggableCore.Entity;
using Goofy.Application.PluggableCore.DTO;
using Goofy.Presentation.Core.Controllers;
using Microsoft.AspNet.Mvc;
using Goofy.Presentation.PluggableCore.Providers;
using Goofy.Domain.PluggableCore.Service.Adapter;

namespace Goofy.Presentation.PluggableCore.Controllers
{
    [Route("api/plugin/PluginItems")]
    public class PluginsReadOnlyController : BaseReadOnlyController<Plugin, PluginItem, int>
    {
        public PluginsReadOnlyController(IPluginServiceMapper<Plugin, PluginItem> service, PluginContextProvider provider)
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
