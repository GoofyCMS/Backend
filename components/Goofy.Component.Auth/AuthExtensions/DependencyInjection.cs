using Microsoft.AspNet.Identity.EntityFramework;

using Goofy.WebFramework.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Goofy.Component.Auth.AuthExtensions
{
    public static class DependencyInjection
    {
        public static void AddIdentity<TUser, TRole, TContext>(this IWebDependencyContainer dependencyContainer) where TUser : IdentityUser where TRole : IdentityRole where TContext : IdentityDbContext<TUser>
        {
            dependencyContainer.ServiceCollection.AddIdentity<TUser, TRole>()
                .AddEntityFrameworkStores<TContext>()
                .AddDefaultTokenProviders();
        }
    }
}
