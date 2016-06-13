using Goofy.Domain.Administration.Entity;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace Goofy.Presentation.Administration.Controllers
{
    [Route("api/administration/user")]
    public class UserController : Controller
    {
        private readonly RoleManager<GoofyRole> _roleManager;
        private readonly UserManager<GoofyUser> _userManager;

        public UserController(UserManager<GoofyUser> userManager, RoleManager<GoofyRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize(Policy = "RequireUpdateGoofyUser")]
        [HttpPost("role/{id}")]
        public IActionResult AddRoles(string id, [FromBody] string[] roles)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByIdAsync(id).Result;
                if (user == null)
                    return HttpNotFound("Invalid User");
                var result = _userManager.AddToRolesAsync(user, roles).Result;
                if (!result.Succeeded)
                    return new ObjectResult(result.Errors.First().Description);
                return Ok();
            }
            return HttpBadRequest();
        }
    }
}
