using System.Threading.Tasks;

using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Authorization;

using Goofy.Component.Authentication.Models;
using Goofy.Component.IdentityIntegration.Models;

namespace Goofy.Component.Authentication.Controllers
{
    public class LoginController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _loginManager;

        public LoginController(SignInManager<ApplicationUser> loginManager, UserManager<ApplicationUser> userManager)
        {
            _loginManager = loginManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Login model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await _loginManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
                //SignInResult
                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            return HttpUnauthorized();
        }


        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _loginManager.SignOutAsync();
            return Ok();
        }
    }
}
