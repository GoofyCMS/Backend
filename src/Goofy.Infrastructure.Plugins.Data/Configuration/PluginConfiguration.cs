using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;

using Goofy.Domain.Plugins.Entity;
using Goofy.Infrastructure.Core.Data.Entity.Configuration;

namespace Goofy.Infrastructure.Plugins.Data.Configuration
{
    public class PluginConfiguration : EntityConfiguration<Plugin>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Plugin> pluginTypeBuilder)
        {
            pluginTypeBuilder.ToTable("Goofy_Component");
            pluginTypeBuilder.HasKey(c => c.ComponentId);
            pluginTypeBuilder.Property(c => c.FullName).IsRequired();
            pluginTypeBuilder.Property(c => c.Installed).IsRequired();
            pluginTypeBuilder.Property(c => c.Version).IsRequired();
            pluginTypeBuilder.Property(c => c.IsSystemComponent).IsRequired();
        }
    }
}
