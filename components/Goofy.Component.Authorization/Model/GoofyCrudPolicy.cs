using System;
using Microsoft.AspNet.Authorization;

namespace Goofy.Component.Authorization.Model
{
    public class GoofyCrudPolicy
    {
        public GoofyCrudPolicy(string policynName, string claimType)
        {
            PolicyName = policynName;
            ClaimType = claimType;
        }
        public string PolicyName { get; }
        public string ClaimType { get; }
        public Action<AuthorizationPolicyBuilder> PolicyBuilder
        {
            get
            {
                return p => p.RequireClaim(ClaimType);
            }
        }
    }
}
