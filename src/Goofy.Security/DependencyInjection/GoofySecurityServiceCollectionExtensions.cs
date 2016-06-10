using Microsoft.Extensions.DependencyInjection;
using Goofy.Security.UserModel;
using Microsoft.AspNet.Identity.EntityFramework6;
using Goofy.Configuration;
using Microsoft.Extensions.OptionsModel;
using Goofy.Security.Services;
using Goofy.Security.Extensions;
using Microsoft.AspNet.Identity;

namespace Goofy.Security.DependencyInjection
{
    public static class GoofySecurityServiceCollectionExtensions
    {
        public static IServiceCollection AddGoofySecurity(this IServiceCollection services)
        {
            services.AddScoped<IdentityDbContext<GoofyUser>>(context =>
            {
                var connectionString = context.GetRequiredService<IOptions<DataAccessConfiguration>>().Value.ConnectionString;
                return new GoofyDbContext(connectionString);
            });
            //Configure Identity middleware with ApplicationUser and the EF6 IdentityDbContext
            services.AddIdentity<GoofyUser, GoofyRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Cookies.ApplicationCookie.CookieName = "GoofyFramework";
                config.Cookies.ApplicationCookie.LoginPath = new Microsoft.AspNet.Http.PathString("/administration/forbidden");
                config.Cookies.ApplicationCookie.AccessDeniedPath = new Microsoft.AspNet.Http.PathString("/administration/forbidden");
            })
            .AddEntityFrameworkStores<IdentityDbContext<GoofyUser>>()
            .AddRoleManager<GoofyRoleManager<GoofyRole>>()
            .AddDefaultTokenProviders();

            services.AddScoped<IUserSign, UserSign>();
            services.AddScoped<IUserRegister, UserRegister>();

            services.AddCrudPermissions("GoofyUser", "GoofyRole");
            services.BuildPermissions();
            return services;
        }
    }
}
