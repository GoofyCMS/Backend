using Goofy.Application.Administration.DTO;
using Goofy.Domain.Administration.Service.Adapter;
using Goofy.Domain.Identity.Entity;
using Goofy.Presentation.Administration.Providers;
using Goofy.Presentation.Core.Controllers;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace Goofy.Presentation.Administration.Controllers
{
    [Route("api/administration/IdentityRoleClaimItems")]
    public class IdentityRoleClaimController : BaseController<IdentityRoleClaim, IdentityRoleClaimItem, int>
    {
        public IdentityRoleClaimController(IAdministrationServiceMapper<IdentityRoleClaim, IdentityRoleClaimItem> service, AdministrationContextProvider provider)
            : base(service, provider)
        {
        }

        [Authorize(Policy = "RequireReadIdentityRoleClaim")]
        public override IActionResult Get()
        {
            return base.Get();
        }
    }
}
