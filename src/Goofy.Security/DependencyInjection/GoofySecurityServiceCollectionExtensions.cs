using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Identity;
using Goofy.Security.UserModel;

namespace Goofy.Security.DependencyInjection
{
    public static class GoofySecurityServiceCollectionExtensions
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>();
            //services.AddSingleton<UserManager<IdentityUser>>();
            //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IdentityDbContext>();
            //services.AddSingleton<IdentityDbContext<IdentityUser>>();
            //services.AddSingleton<SignInManager>();
            //            private readonly UserManager<ApplicationUser> _userManager;
            //private readonly SignInManager<ApplicationUser> _signInManager;
            return services;
        }
    }
}
