using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

using Goofy.Core.Infrastructure;
using Goofy.Data;

using Goofy.Component.Auth.AuthExtensions;
using Goofy.Component.Auth.Models;

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

        public void Register(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole, UserDbContext>();
            services.AddDbContextObject<UserDbContext>();
        }
    }
}
