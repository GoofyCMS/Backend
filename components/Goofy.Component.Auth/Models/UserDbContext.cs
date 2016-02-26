using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

using Goofy.Data;

namespace Goofy.Component.Auth.Models
{
    public class UserDbContext : IdentityDbContext<ApplicationUser>
    {
        public UserDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            GoofyDataAccessManager.GoofyDataConfiguration.Provider.ConfigureDbContextProvider(optionsBuilder);
        }
    }
}
