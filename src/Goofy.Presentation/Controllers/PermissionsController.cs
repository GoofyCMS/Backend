using Microsoft.AspNet.Mvc;
using Goofy.Presentation.Configuration.Models;
using Goofy.Security.Services;

namespace Goofy.Presentation.Controllers
{
    public class PermissionsController : Controller
    {
        private readonly AuthorizationService _authorizationService;

        public PermissionsController(AuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        // GET: /<controller>/
        [HttpPost("permissions")]
        public IActionResult GetPermissions([FromBody] PermissionRequestViewModel requestViewModel)
        {
            if (ModelState.IsValid)
            {
                var permissions = _authorizationService.GetPermissions(User, requestViewModel.Permissions);

                return new ObjectResult(permissions);

            }
            return new BadRequestResult();
        }
    }
}
