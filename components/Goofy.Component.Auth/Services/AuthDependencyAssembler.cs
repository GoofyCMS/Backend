using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

using Goofy.Component.Auth.AuthExtensions;
using Goofy.Component.Auth.Models;
using Goofy.Core.Infrastructure;
using Goofy.WebFramework.Data.DependencyInjection;

namespace Goofy.Component.Auth.Services
{
    public class AuthDependencyAssembler : IDependencyAssembler
    {
        public int Order
        {
            get
            {
                return 0;
            }
        }

        public void Register(IServiceCollection services, IResourcesLoader loader)
        {
            services.AddIdentity<ApplicationUser, IdentityRole, UserDbContext>();
            services.AddDbContextObject<UserDbContext>();
        }
    }
}
