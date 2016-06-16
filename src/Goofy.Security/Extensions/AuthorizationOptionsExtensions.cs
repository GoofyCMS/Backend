using Microsoft.AspNet.Authorization;
using System.Collections.Generic;
using System.Security.Claims;

namespace Goofy.Security.Extensions
{
    public static class AuthorizationOptionsExtensions
    {
        public static void AddResourceCrudPermissions(this AuthorizationOptions options, KeyValuePair<string, IEnumerable<CrudOperation>> resourcePermission)
        {
            foreach (var permission in resourcePermission.Value)
            {
                options.AddPolicy(SecurityUtils.GetPolicyName(resourcePermission.Key, permission), policy => policy.Requirements.Add(new CustomRequireClaimRequirement(new Claim(SecurityUtils.GetPermissionName(resourcePermission.Key, permission), ""))));
            }
        }
    }
}
