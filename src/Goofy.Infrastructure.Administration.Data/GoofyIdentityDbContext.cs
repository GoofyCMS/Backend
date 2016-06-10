using System.Data.Entity;
using Goofy.Domain.Administration.Entity;
using Goofy.Infrastructure.Identity.Data;

namespace Goofy.Infrastructure.Administration.Data
{
    public class GoofyIdentityDbContext : IdentityDbContext<GoofyUser>
    {
        public GoofyIdentityDbContext(string connectionString)
            : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var role = builder.Entity<GoofyRole>();
            role.Property(t => t.Description).HasMaxLength(256);
        }
    }
}
