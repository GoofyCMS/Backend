using Goofy.Application.Administration.DTO;
using Goofy.Domain.Identity.Entity;
using Goofy.Domain.Identity.Services.Adapter;
using Goofy.Presentation.Administration.Providers;
using Goofy.Presentation.Core.Controllers;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace Goofy.Presentation.Administration.Controllers
{
    [Route("api/administration/IdentityUserRoleItems")]
    public class IdentityUserRoleController : BaseController<IdentityUserRole, IdentityUserRoleItem, int>
    {
        public IdentityUserRoleController(IAdministrationServiceMapper<IdentityUserRole, IdentityUserRoleItem> service, AdministrationContextProvider provider)
            : base(service, provider)
        {
        }

        //[Authorize(Policy = "RequireReadIdentityUserRole")]
        //public override IActionResult Get()
        //{
        //    return base.Get();
        //}
    }
}
