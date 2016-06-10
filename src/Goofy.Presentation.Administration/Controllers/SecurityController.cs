using Goofy.Domain.Administration.Entity;
using Goofy.Presentation.Administration.ViewModels;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using System.Threading.Tasks;

namespace Goofy.Presentation.PluggableCore.Controllers
{
    [Authorize]
    [Route("administration")]
    public class SecurityController : Controller
    {
        private readonly SignInManager<GoofyUser> _signInManager;
        private readonly UserManager<GoofyUser> _userManager;

        public SecurityController(SignInManager<GoofyUser> signInManager, UserManager<GoofyUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet("create_admin")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser()
        {
            var user = new GoofyUser
            {
                UserName = "cyberboy.havana@gmail.com",
                Email = "cyberboy.havana@gmail.com"
            };
            var result = await _userManager.CreateAsync(user, "Admin!234");
            if (result.Succeeded)
            {
                return Ok();
            }
            return new ObjectResult(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn([FromBody]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
                //SignInResult
                if (result.Succeeded)
                {
                    return Ok();
                }
                return new ObjectResult(result);
            }
            return HttpBadRequest();
        }

        [HttpGet("forbidden")]
        [AllowAnonymous]
        public IActionResult Forbidden()
        {
            return ForbiddenResult();
        }

        private HttpStatusCodeResult ForbiddenResult()
        {
            return new HttpStatusCodeResult(403);
        }

        [HttpGet("test_something")]
        public string Test()
        {
            return "this is a testing action";
        }

        [HttpPost("logoff")]
        public async Task LogOff()
        {
            await _signInManager.SignOutAsync();
        }

    }
}
