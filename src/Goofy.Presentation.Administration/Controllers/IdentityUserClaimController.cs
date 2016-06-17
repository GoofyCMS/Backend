using Goofy.Application.Administration.DTO;
using Goofy.Domain.Administration.Service.Adapter;
using Goofy.Domain.Identity.Entity;
using Goofy.Presentation.Administration.Providers;
using Goofy.Presentation.Core.Controllers;
using Microsoft.AspNet.Mvc;

namespace Goofy.Presentation.Administration.Controllers
{
    [Route("api/administration/IdentityUserClaimItems")]
    public class IdentityUserClaimController : BaseController<IdentityUserClaim, IdentityUserClaimItem, int>
    {
        public IdentityUserClaimController(IAdministrationServiceMapper<IdentityUserClaim, IdentityUserClaimItem> service, AdministrationContextProvider provider)
            : base(service, provider)
        {
        }
    }
}
