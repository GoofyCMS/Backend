using Microsoft.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

using Goofy.Data.DataProvider.Services;

namespace Goofy.Component.IdentityIntegration.Models
{
    public class UserDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IDataProviderConfigurator _providerConfigurator;

        public UserDbContext(IDataProviderConfigurator dataProviderConfigurator)
        {
            _providerConfigurator = dataProviderConfigurator;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            _providerConfigurator.ConfigureDbContextProvider(optionsBuilder);
        }
    }
}
