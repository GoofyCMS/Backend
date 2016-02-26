using Goofy.Core.Infrastructure;
using Goofy.WebFramework.Infrastructure;
using Microsoft.Extensions.Configuration;
using Goofy.Component.Auth.Models;
using Goofy.Component.Auth.AuthExtensions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Goofy.Component.Auth.Services
{
    public class AuthDependencyAssembler : IGoofyDependencyAssembler
    {
        public int Order
        {
            get
            {
                return 0;
            }
        }

        public void Register(IDependencyContainer builder, IResourcesLoader loader)
        {
        }

        public void RegisterWebDependencies(IWebDependencyContainer container, IResourcesLoader loader, IConfiguration config)
        {
            container.AddDbContextObject<UserDbContext>();
            container.AddIdentity<ApplicationUser, IdentityRole, UserDbContext>();
        }
    }
}
