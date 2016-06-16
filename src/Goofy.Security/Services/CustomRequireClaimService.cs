
using Goofy.Security.Services.Abstractions;
using System.Linq;
using System.Security.Claims;

namespace Goofy.Security.Services
{
    public class CustomRequireClaimService
    {
        private readonly IRoleClaimProvider _roleClaimProvider;

        public CustomRequireClaimService(IRoleClaimProvider roleClaimProvider)
        {
            _roleClaimProvider = roleClaimProvider;
        }

        public bool UserHasClaim(ClaimsPrincipal user, Claim claim)
        {
            if (user.HasClaim(c => ClaimEqual(claim, c)))
            {
                return true;
            }

            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(r => r.Value).ToArray();

            if (_roleClaimProvider.RoleClaims(roles).Any(c => ClaimEqual(claim, c)))
            {
                return true;
            }
            return false;
        }

        private static bool ClaimEqual(Claim first, Claim second)
        {
            return second.Type == first.Type && second.Value == first.Value;
        }
    }
}
