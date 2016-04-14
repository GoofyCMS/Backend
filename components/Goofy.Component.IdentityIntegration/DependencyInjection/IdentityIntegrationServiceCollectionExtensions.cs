
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Goofy.Component.IdentityIntegration.DependencyInjection
{
    public static class IdentityIntegrationServiceCollectionExtensions
    {
        public static void AddIdentity<TUser, TRole, TContext>(this IServiceCollection services)
                                                               where TUser : IdentityUser
                                                               where TRole : IdentityRole
                                                               where TContext : IdentityDbContext<TUser>
        {
            services.AddIdentity<TUser, TRole>()
                .AddEntityFrameworkStores<TContext>()
                .AddDefaultTokenProviders();
        }
    }
}
