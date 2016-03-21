using Microsoft.AspNet.Identity.EntityFramework;

using Microsoft.Extensions.DependencyInjection;

namespace Goofy.Component.Auth.AuthExtensions
{
    public static class DependencyInjection
    {
        public static void AddIdentity<TUser, TRole, TContext>(this IServiceCollection services) where TUser : IdentityUser where TRole : IdentityRole where TContext : IdentityDbContext<TUser>
        {
            services.AddIdentity<TUser, TRole>()
                .AddEntityFrameworkStores<TContext>()
                .AddDefaultTokenProviders();
        }
    }
}
