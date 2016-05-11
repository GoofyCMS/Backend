using System.Data.Entity;
using Goofy.Application.Plugins.DTO;

namespace Goofy.Web.Plugins.Providers
{
    public class PluginMetadataContext : DbContext
    {
        static PluginMetadataContext()
        {
            // Prevent attempt to initialize a database for this context
            Database.SetInitializer<PluginMetadataContext>(null);
        }

        public virtual IDbSet<PluginItem> Plugins { get; set; }
    }
}
