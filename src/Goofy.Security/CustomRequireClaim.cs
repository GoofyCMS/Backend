using Microsoft.AspNet.Authorization;

namespace Goofy.Security.Extensions
{
    public class CustomRequireClaim : IAuthorizationRequirement
    {

        public CustomRequireClaim(string claimType)
        {
            ClaimType = claimType;
        }

        public string ClaimType { get; private set; }
    }
}