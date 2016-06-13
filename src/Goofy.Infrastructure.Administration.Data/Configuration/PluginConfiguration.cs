using Goofy.Domain.Administration.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Goofy.Infrastructure.Administration.Data.Configuration
{
    public class PluginConfiguration : EntityTypeConfiguration<Plugin>
    {
        public PluginConfiguration()
        {
            ToTable("Goofy_Plugins");
            HasKey(c => c.Id);
            //Property(c => c.FullName).IsRequired();
            Property(c => c.Name).IsRequired();
            Property(c => c.Enabled).IsRequired();
            //Property(c => c.Version).IsRequired();
            //Property(c => c.IsSystemComponent).IsRequired();
        }
    }
}
