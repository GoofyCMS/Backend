using Goofy.Application.Administration.DTO;
using Goofy.Domain.Administration.Entity;
using Goofy.Domain.Identity.Services.Adapter;
using Goofy.Presentation.Administration.Providers;
using Goofy.Presentation.Core.Controllers;
using Microsoft.AspNet.Authorization;
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

        //[Authorize(Policy = "RequireReadPlugin")]
        //public override IActionResult Get()
        //{
        //    return base.Get();
        //}
    }
}
