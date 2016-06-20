using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using Goofy.Presentation.Administration.ViewModels;
using Goofy.Domain.Administration.Entity;
using System.Linq;
using Microsoft.AspNet.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Goofy.Presentation.Administration.Controllers
{
    [Authorize(Policy = "RequireCreateGoofyUser")]
    [Route("api/administration")]
    public class RegisterController : Controller
    {
        private readonly UserManager<GoofyUser> _userManager;

        public RegisterController(UserManager<GoofyUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new GoofyUser
                {
                    UserName = loginViewModel.Username,
                    Email = loginViewModel.Username,
                };
                var result = _userManager.CreateAsync(user, loginViewModel.Password).Result;
                return new ObjectResult(new { success = result.Succeeded, errors = result.Errors.Select(e => e.Description) });
            }
            return new BadRequestResult();
        }
    }
}
