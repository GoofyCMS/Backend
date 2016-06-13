using Goofy.Application.Administration.DTO;
using Goofy.Domain.Administration.Entity;
using Goofy.Domain.Administration.Service.Adapter;
using Goofy.Presentation.Administration.Providers;
using Goofy.Presentation.Core.Controllers;
using Microsoft.AspNet.Mvc;

namespace Goofy.Presentation.Administration.Controllers
{
    [Route("api/administration/PluginItems")]
    public class PluginsReadOnlyController : BaseReadOnlyController<Plugin, PluginItem, int>
    {
        public PluginsReadOnlyController(IAdministrationServiceMapper<Plugin, PluginItem> service, AdministrationContextProvider provider)
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
