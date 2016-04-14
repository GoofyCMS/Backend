using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Identity.EntityFramework;

using Goofy.Core.Infrastructure;
using Goofy.Data;
using Goofy.Component.IdentityIntegration.Models;
using Goofy.Component.IdentityIntegration.Configuration;
using Goofy.Component.IdentityIntegration.DependencyInjection;

namespace Goofy.Component.IdentityIntegration.Services
{
    public class IdentityIntegrationDependencyAssembler : IDependencyAssembler
    {
        public int Order
        {
            get
            {
                return 0;
            }
        }

        public void Register(IServiceCollection services)
        {
            services.Configure<IdentityIntegrationConfiguration>(c => { });
            services.AddIdentity<ApplicationUser, IdentityRole, UserDbContext>();
            services.AddDbContextObject<UserDbContext>();
        }
    }
}
