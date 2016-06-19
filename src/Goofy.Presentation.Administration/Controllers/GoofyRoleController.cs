using Goofy.Application.Administration.DTO;
using Goofy.Domain.Administration.Entity;
using Goofy.Domain.Administration.Service.Adapter;
using Goofy.Presentation.Administration.Providers;
using Goofy.Presentation.Core.Controllers;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace Goofy.Presentation.Administration.Controllers
{
    [Route("api/administration/GoofyRoleItems")]
    public class GoofyRoleController : BaseController<GoofyRole, GoofyRoleItem, int>
    {
        public GoofyRoleController(IAdministrationServiceMapper<GoofyRole, GoofyRoleItem> service, AdministrationContextProvider provider)
            : base(service, provider)
        {
        }

        [Authorize(Policy = "RequireReadGoofyRole")]
        public override IActionResult Get()
        {
            return base.Get();
        }
    }
}
