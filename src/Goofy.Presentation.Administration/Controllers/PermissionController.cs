using Goofy.Application.Administration.DTO;
using Goofy.Domain.Administration.Entity;
using Goofy.Domain.Administration.Service.Adapter;
using Goofy.Presentation.Administration.Providers;
using Goofy.Presentation.Core.Controllers;
using Microsoft.AspNet.Mvc;

namespace Goofy.Presentation.Administration.Controllers
{
    [Route("api/administration/PermissionItems")]
    public class PermissionController : BaseReadOnlyController<Permission, PermissionItem, int>
    {
        public PermissionController(IAdministrationServiceMapper<Permission, PermissionItem> service, AdministrationContextProvider provider)
            : base(service, provider)
        {
        }
    }
}
