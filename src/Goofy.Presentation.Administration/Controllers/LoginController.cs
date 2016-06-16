using Goofy.Domain.Administration.Entity;
using Goofy.Presentation.Administration.ViewModels;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using System.Threading.Tasks;

namespace Goofy.Presentation.Administration.Controllers
{
    [Authorize]
    [Route("api/administration")]
    public class LoginController : Controller
    {
        private readonly SignInManager<GoofyUser> _signInManager;
        private readonly UserManager<GoofyUser> _userManager;

        public LoginController(SignInManager<GoofyUser> signInManager, UserManager<GoofyUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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

        [HttpGet("test_other")]
        [Authorize(Roles = "Administrator")]
        public string OtherTest()
        {
            return "this is other test";
        }

        [HttpGet("test_policies")]
        [Authorize(Policy = "RequireDeleteArticle")]
        public string OtherTestMore()
        {
            return "Yeah I fucking finish basic security features...";
        }

        [HttpPost("logoff")]
        public async Task LogOff()
        {
            await _signInManager.SignOutAsync();
        }

    }
}
