using Goofy.Application.Administration.DTO;
using System.Data.Entity;

namespace Goofy.Presentation.Administration.Providers
{
    public class AdministrationMetadataContext : DbContext
    {
        static AdministrationMetadataContext()
        {
            // Prevent attempt to initialize a database for this context
            Database.SetInitializer<AdministrationMetadataContext>(null);
        }

        public virtual IDbSet<PluginItem> PluginItems { get; set; }
        public virtual IDbSet<GoofyRoleItem> GoofyRoleItems { get; set; }
        public virtual IDbSet<IdentityRoleClaimItem> IdentityRoleClaimItems { get; set; }
        public virtual IDbSet<GoofyUserItem> GoofyUserItems { get; set; }
        public virtual IDbSet<IdentityUserClaimItem> IdentityUserClaimItems { get; set; }
        public virtual IDbSet<IdentityUserRoleItem> IdentityUserRoleItems { get; set; }
        public virtual IDbSet<PermissionItem> PermissionItems { get; set; }
    }
}
