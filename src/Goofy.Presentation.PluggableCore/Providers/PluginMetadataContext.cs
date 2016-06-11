using System.Data.Entity;
using Goofy.Application.PluggableCore.DTO;

namespace Goofy.Presentation.PluggableCore.Providers
{
    public class PluginMetadataContext : DbContext
    {
        static PluginMetadataContext()
        {
            // Prevent attempt to initialize a database for this context
            Database.SetInitializer<PluginMetadataContext>(null);
        }

        public virtual IDbSet<PluginItem> PluginItems { get; set; }
    }
}
