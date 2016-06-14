using Goofy.Domain.Administration.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Goofy.Infrastructure.Administration.Data.Configuration
{
    public class GoofyRoleConfiguration : EntityTypeConfiguration<GoofyRole>
    {
        public GoofyRoleConfiguration()
        {
            Property(e => e.Description).HasMaxLength(256);
        }
    }
}
