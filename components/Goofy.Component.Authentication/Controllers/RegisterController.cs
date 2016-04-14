using System.Threading.Tasks;

using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Authorization;

using Goofy.Component.Authentication.Models;
using Goofy.Component.IdentityIntegration.Models;

namespace Goofy.Component.Authentication.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _loginManager;

        public RegisterController(SignInManager<ApplicationUser> loginManager, UserManager<ApplicationUser> userManager)
        {
            _loginManager = loginManager;
            _userManager = userManager;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _loginManager.SignInAsync(user, isPersistent: false);
                    return Ok();
                }
            }
            return HttpUnauthorized();
        }
    }
}
