using Goofy.Application.Administration.DTO;
using Goofy.Domain.Administration.Service.Adapter;
using Goofy.Domain.Identity.Entity;
using Goofy.Presentation.Administration.Providers;
using Goofy.Presentation.Core.Controllers;
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
    }
}
