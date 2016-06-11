
using Goofy.Security.Entity;
using Microsoft.AspNet.Authorization;

namespace Goofy.Security.Extensions
{
    public static class AuthorizationOptionsExtensions
    {
        public static void AddResourceCrudPermissions(this AuthorizationOptions options, string resource)
        {
            options.AddPolicy(SecurityUtils.GetPolicyName(resource, CrudOperation.Create), policy => policy.RequireClaim(SecurityUtils.GetPermissionName(resource, CrudOperation.Create)));
            options.AddPolicy(SecurityUtils.GetPolicyName(resource, CrudOperation.Read), policy => policy.RequireClaim(SecurityUtils.GetPermissionName(resource, CrudOperation.Read)));
            options.AddPolicy(SecurityUtils.GetPolicyName(resource, CrudOperation.Update), policy => policy.RequireClaim(SecurityUtils.GetPermissionName(resource, CrudOperation.Update)));
            options.AddPolicy(SecurityUtils.GetPolicyName(resource, CrudOperation.Delete), policy => policy.RequireClaim(SecurityUtils.GetPermissionName(resource, CrudOperation.Delete)));
        }
    }
}
