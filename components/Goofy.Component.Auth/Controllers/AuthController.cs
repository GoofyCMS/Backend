using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Goofy.Component.Auth.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Cors;

namespace Goofy.Component.Auth.Controllers
{
    [Route("auth")]
    [EnableCors("AllowYoel")]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _loginManager;

        public AuthController(SignInManager<ApplicationUser> loginManager, UserManager<ApplicationUser> userManager)
        {
            _loginManager = loginManager;
            _userManager = userManager;
        }


        [HttpPost("register")]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] Register model)//Preguntar si aquí se hace chequeo contra robots.
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


        [HttpPost("login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _loginManager.SignOutAsync();
            return Ok();
        }

        [HttpGet("pepe")]
        public IActionResult Simple()
        {
            return new ObjectResult(1);
        }
    }
}
