using Goofy.Domain.Administration.Entity;
using Goofy.Domain.Administration.Service.Data;
using Goofy.Infrastructure.Administration.Data.Configuration;
using Goofy.Infrastructure.Identity.Data.Service;
using System.Data.Entity;

namespace Goofy.Infrastructure.Administration.Data
{
    public class AdministrationContext : IdentityUnitOfWork<GoofyUser>, IAdministrationUnitOfWork
    {
        public AdministrationContext(string connectionString)
            : base(connectionString)
        {
        }

        public virtual DbSet<Plugin> Plugins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new PluginConfiguration());
            modelBuilder.Configurations.Add(new GoofyRoleConfiguration());
            modelBuilder.Configurations.Add(new PermissionConfiguration());
        }
    }
}
