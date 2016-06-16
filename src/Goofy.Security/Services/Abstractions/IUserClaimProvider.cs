using System.Collections.Generic;
using System.Security.Claims;

namespace Goofy.Security.Services.Abstractions
{
    public interface IUserClaimProvider
    {
        IEnumerable<Claim> GetUserClaims(string userName, string password);
    }
}
