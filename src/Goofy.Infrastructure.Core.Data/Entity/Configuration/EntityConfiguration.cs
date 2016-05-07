using Microsoft.Data.Entity.Metadata.Builders;
using Goofy.Domain.Core.Entity;

namespace Goofy.Infrastructure.Core.Data.Entity.Configuration
{
    public abstract class EntityConfiguration<TEntity> where TEntity : BaseEntity
    {
        public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> entityTypeBuilder);
    }
}
