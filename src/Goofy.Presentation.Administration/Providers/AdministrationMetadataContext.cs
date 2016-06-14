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
        public virtual IDbSet<GoofyRoleItem> GoofyUserItems { get; set; }
        public virtual IDbSet<IdentityRoleClaimItem> IdentityRoleClaimItems { get; set; }
    }
}
