using System.Collections.Generic;
using System.Security.Claims;

namespace Goofy.Security.Services.Abstractions
{
    public interface IRoleClaimProvider
    {
        IEnumerable<Claim> RoleClaims(IEnumerable<string> roles);
    }
}
