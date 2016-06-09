using Goofy.Security.Services;
using Goofy.Security.ViewModel;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using System.Threading.Tasks;

namespace Goofy.Presentation.PluggableCore.Controllers
{
    [Authorize]
    [Route("administration")]
    public class SecurityController : Controller
    {
        private readonly IUserSign _userSign;
        private readonly IUserRegister _userRegister;

        public SecurityController(IUserSign userSign, IUserRegister userRegister)
        {
            _userSign = userSign;
            _userRegister = userRegister;
        }

        [HttpGet("create_admin")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser()
        {
            await _userRegister.Register("cyberboy.havana@gmail.com", "Admin!234");
            return Ok();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn([FromBody]Login model)
        {
            if (ModelState.IsValid)
            {
                var success = await _userSign.Login(model.Email, model.Password);
                //SignInResult
                if (success)
                {
                    return Ok();
                }
            }
            return HttpUnauthorized();
        }

        [HttpGet("test_something")]
        public string Test()
        {
            return "this is a testing action";
        }

        [HttpPost("logoff")]
        public async Task LogOff()
        {
            await _userSign.LogOff();
        }

    }
}
