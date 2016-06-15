using System.Collections.Generic;
using System.Security.Claims;
using Goofy.Security.Services.Abstractions;
using Microsoft.AspNet.Identity;
using Goofy.Domain.Administration.Entity;

namespace Goofy.Application.Administration.Services
{
    public class UserClaimsProvider : IUserClaimProvider
    {
        private readonly UserManager<GoofyUser> _userManager;

        public UserClaimsProvider(UserManager<GoofyUser> userManager)
        {
            _userManager = userManager;
        }

        public IEnumerable<Claim> GetUserClaims(string userName, string password)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            if (user == null)
                return null;
            return _userManager.GetClaimsAsync(user).Result;
        }
    }
}
