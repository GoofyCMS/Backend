using System;
using Goofy.Security.Extensions;
using Microsoft.AspNet.Authorization;
using System.Linq;
using System.Security.Claims;
using Goofy.Security.Services.Abstractions;

namespace Goofy.Security.Services
{
    public class CustomRequireClaimHandler : AuthorizationHandler<CustomRequireClaimRequirement>
    {
        private readonly CustomRequireClaimService _requireClaimService;

        public CustomRequireClaimHandler(CustomRequireClaimService requireClaimService)
        {
            _requireClaimService = requireClaimService;
        }

        protected override void Handle(AuthorizationContext context, CustomRequireClaimRequirement requirement)
        {
            if (_requireClaimService.UserHasClaim(context.User, requirement.Claim))
                context.Succeed(requirement);
        }
    }
}