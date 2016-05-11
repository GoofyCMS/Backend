using Goofy.Domain.Plugins.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Goofy.Infrastructure.Plugins.Data.Configuration
{
    public class PluginConfiguration : EntityTypeConfiguration<Plugin>
    {
        public PluginConfiguration()
        {
            ToTable("Goofy_Plugins");
            HasKey(c => c.PluginId);
            //Property(c => c.FullName).IsRequired();
            Property(c => c.Installed).IsRequired();
            //Property(c => c.Version).IsRequired();
            //Property(c => c.IsSystemComponent).IsRequired();
        }
    }
}
