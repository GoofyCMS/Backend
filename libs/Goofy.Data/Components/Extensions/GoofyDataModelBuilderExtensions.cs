using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;
using Goofy.Core.Entity.Base;

namespace Goofy.Data.Components.Extensions
{
    public static class GoofyDataModelBuilderExtensions
    {
        public static ModelBuilder GoofyEntity<T>(this ModelBuilder modelBuilder, Action<EntityTypeBuilder<T>> buildAction) where T : GoofyEntityBase
        {
            modelBuilder.Entity<T>(typeBuilder =>
            {
                typeBuilder.Property(ge => ge.GlobalId);
                buildAction(typeBuilder);
            });
            return modelBuilder;
        }
    }
}
