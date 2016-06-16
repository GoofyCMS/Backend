using System;
using Goofy.Security.Extensions;
using Microsoft.AspNet.Authorization;
using System.Linq;
using System.Security.Claims;
using Goofy.Security.Services.Abstractions;

namespace Goofy.Security.Services
{
    public class CustomRequireClaimHandler : AuthorizationHandler<CustomRequireClaim>
    {
        private readonly IRoleClaimProvider _roleClaimProvider;

        public CustomRequireClaimHandler(IRoleClaimProvider roleClaimProvider)
        {
            _roleClaimProvider = roleClaimProvider;
        }

        protected override void Handle(AuthorizationContext context, CustomRequireClaim requirement)
        {
            if (context.User.HasClaim(c => c.Type == requirement.ClaimType))
            {
                context.Succeed(requirement);
                return;
            }
            var roles = context.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(r => r.Value).ToArray();
            if (_roleClaimProvider.RoleClaims(roles).Any(c => c.Type == requirement.ClaimType))
            {
                context.Succeed(requirement);
            }
        }
    }
}