using Microsoft.AspNet.Authorization;
using System.Collections.Generic;

namespace Goofy.Security.Extensions
{
    public static class AuthorizationOptionsExtensions
    {
        public static void AddResourceCrudPermissions(this AuthorizationOptions options, KeyValuePair<string, IEnumerable<CrudOperation>> resourcePermission)
        {
            foreach (var permission in resourcePermission.Value)
            {
                options.AddPolicy(SecurityUtils.GetPolicyName(resourcePermission.Key, permission), policy => policy.Requirements.Add(new CustomRequireClaim(SecurityUtils.GetPermissionName(resourcePermission.Key, permission))));
            }
        }
    }
}
