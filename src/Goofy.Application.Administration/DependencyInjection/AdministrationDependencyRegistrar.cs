using System.Reflection;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;
using Goofy.Security.Extensions;
using Goofy.Domain.Administration.Entity;
using Goofy.Application.Administration.Services;
using Goofy.Application.Identity.Extensions;
using Goofy.Infrastructure.Administration.Data;
using Goofy.Infrastructure.Identity.Data.Service;
using Goofy.Domain.Administration.Service.Data;
using Goofy.Infrastructure.Core.Data.Extensions;
using Goofy.Infrastructure.Core.Data;
using Goofy.Application.Core.Abstractions;
using Goofy.Application.Administration.Services.Abstractions;
using Goofy.Security.Services.Abstractions;
using Goofy.Domain.Identity.Entity;
using Goofy.Security;
using Goofy.Security.DependencyInjection;
using Goofy.Domain.Identity.Services.Adapter;

namespace Goofy.Application.Administration.DependencyInjection
{
    public class AdministrationDependencyRegistrar : IDependencyRegistrar
    {
        public void ConfigureServices(IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddUnitOfWork(typeof(IAdministrationUnitOfWork), typeof(AdministrationContext));
            services.AddScoped(typeof(IAdministrationServiceMapper<,>), typeof(AdministrationServiceMapper<,>));
            services.AddSingleton<IPluginManager, PluginManager>();

            services.AddScoped<IdentityDbContext<GoofyUser>>(context =>
            {
                var connectionString = context.GetRequiredService<IOptions<DataAccessConfiguration>>().Value.ConnectionString;
                return new AdministrationContext(connectionString);
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

            services.AddScoped<IUserClaimProvider, UserClaimProvider>();
            services.AddScoped<IRoleClaimProvider, RoleClaimProvider>();

            services.AddCrudPermissions(typeof(Permission), CrudOperation.Read);
            services.AddCrudPermissions(typeof(Plugin), CrudOperation.Read, CrudOperation.Update);
            services.AddEntireCrudPermissions(typeof(GoofyUser), typeof(GoofyRole), typeof(IdentityRoleClaim), typeof(IdentityUserRole), typeof(IdentityUserClaim));
        }
    }
}
