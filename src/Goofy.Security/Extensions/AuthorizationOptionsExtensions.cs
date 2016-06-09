
using Microsoft.AspNet.Authorization;

namespace Goofy.Security.Extensions
{
    public static class AuthorizationOptionsExtensions
    {
        public static void AddResourceCrudPermissions(this AuthorizationOptions options, string resource)
        {
            options.AddPolicy($"RequireCreate{resource}", policy => policy.RequireClaim($"CanCreate{resource}"));
            options.AddPolicy($"RequireRead{resource}", policy => policy.RequireClaim($"CanRead{resource}"));
            options.AddPolicy($"RequireUpdate{resource}", policy => policy.RequireClaim($"CanUpdate{resource}"));
            options.AddPolicy($"RequireDelete{resource}", policy => policy.RequireClaim($"CanDelete{resource}"));
        }
    }
}
