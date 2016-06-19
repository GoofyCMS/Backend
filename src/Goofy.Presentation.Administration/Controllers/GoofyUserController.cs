using Goofy.Application.Administration.DTO;
using Goofy.Domain.Administration.Entity;
using Goofy.Domain.Administration.Service.Adapter;
using Goofy.Presentation.Administration.Providers;
using Goofy.Presentation.Core.Controllers;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace Goofy.Presentation.Administration.Controllers
{
    [Route("api/administration/GoofyUserItems")]
    public class GoofyUserController : BaseReadOnlyController<GoofyUser, GoofyUserItem, int>
    {
        public GoofyUserController(IAdministrationServiceMapper<GoofyUser, GoofyUserItem> service, AdministrationContextProvider provider)
            : base(service, provider)
        {
        }

        [Authorize(Policy = "RequireReadGoofyUser")]
        public override IActionResult Get()
        {
            return base.Get();
        }
    }
}
