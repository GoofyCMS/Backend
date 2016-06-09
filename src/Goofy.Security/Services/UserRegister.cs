using Goofy.Security.UserModel;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace Goofy.Security.Services
{
    public class UserRegister: IUserRegister
    {
        private readonly UserManager<GoofyUser> _userManager;

        public UserRegister(UserManager<GoofyUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Register(string email, string password)
        {
            var user = new GoofyUser
            {
                UserName = email,
                Email = email
            };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}
