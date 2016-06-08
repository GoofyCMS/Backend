using Microsoft.AspNet.Identity.EntityFramework6;
using System.Data.Entity;

namespace Goofy.Security.UserModel
{
    public class GoofyDbContext : IdentityDbContext<GoofyUser>
    {
        public GoofyDbContext(string connectionString)
            : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
