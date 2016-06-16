using Goofy.Domain.Administration.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Goofy.Infrastructure.Administration.Data.Configuration
{
    public class PermissionConfiguration : EntityTypeConfiguration<Permission>
    {
        public PermissionConfiguration()
        {
            ToTable("Permissions");
            HasKey(c => c.Id);
            Property(c => c.Name)
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(
                    new IndexAttribute("NameIndex") { IsUnique = true }));
        }
    }
}
