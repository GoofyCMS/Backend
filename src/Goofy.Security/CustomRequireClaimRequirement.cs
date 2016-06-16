using Microsoft.AspNet.Authorization;
using System.Security.Claims;

namespace Goofy.Security.Extensions
{
    public class CustomRequireClaimRequirement : IAuthorizationRequirement
    {

        public CustomRequireClaimRequirement(Claim claim)
        {
            Claim = claim;
        }

        public Claim Claim { get; private set; }
    }
}