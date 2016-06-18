using System.Collections.Generic;
using System.Security.Claims;
using Goofy.Security.Services.Abstractions;
using Microsoft.AspNet.Identity;
using Goofy.Domain.Administration.Entity;
using System.Threading.Tasks;

namespace Goofy.Application.Administration.Services
{
    public class UserClaimProvider : IUserClaimProvider
    {
        private readonly UserManager<GoofyUser> _userManager;

        public UserClaimProvider(UserManager<GoofyUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<Claim>> GetUserClaims(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>(new Claim[] { new Claim(ClaimTypes.Name, user.UserName) });
            foreach (var r in roles) claims.Add(new Claim(ClaimTypes.Role, r));
            foreach (var c in user.Claims) claims.Add(new Claim(c.ClaimType, c.ClaimValue));

            return claims;
        }
    }
}
