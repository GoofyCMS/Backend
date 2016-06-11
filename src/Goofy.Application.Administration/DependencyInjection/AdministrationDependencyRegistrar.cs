using Goofy.Application.Administration.Services;
using Goofy.Application.Identity.Extensions;
using Goofy.Application.PluggableCore.Abstractions;
using Goofy.Configuration;
using Goofy.Domain.Administration.Entity;
using Goofy.Infrastructure.Administration.Data;
using Goofy.Infrastructure.Identity.Data.Service;
using Goofy.Security.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;

namespace Goofy.Application.Administration.DependencyInjection
{
    public class AdministrationDependencyRegistrar : IDependencyRegistrar
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IdentityDbContext<GoofyUser>>(context =>
            {
                var connectionString = context.GetRequiredService<IOptions<DataAccessConfiguration>>().Value.ConnectionString;
                return new AdministrationDbContext(connectionString);
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

            services.AddCrudPermissions("GoofyUser", "GoofyRole");
        }
    }
}
