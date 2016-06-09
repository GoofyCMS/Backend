using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Goofy.Security.UserModel;

namespace Goofy.Security.Services
{
    public class UserSign : IUserSign
    {
        private readonly SignInManager<GoofyUser> _loginManager;
        private readonly UserManager<GoofyUser> _userManager;

        public UserSign(SignInManager<GoofyUser> loginManager, UserManager<GoofyUser> userManager)
        {
            _loginManager = loginManager;
            _userManager = userManager;
        }

        public async Task<bool> Login(string email, string password)
        {
            var result = await _loginManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);
            //SignInResult
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task LogOff()
        {
            await _loginManager.SignOutAsync();
        }
    }
}
