using Microsoft.Data.Entity;
using Goofy.Core.Components;
using Goofy.Data.DataProvider.Services;
using Goofy.Data.Components.Extensions;

namespace Goofy.Data.Components
{
    public class ComponentContext : GoofyObjectContext
    {
        public ComponentContext(IDataProviderConfigurator dataProviderConfigurator) : base(dataProviderConfigurator)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.GoofyEntity<Component>(component =>
            {
                component.ToTable("Goofy_Component");
                component.HasKey(c => c.ComponentId);
                component.Property(c => c.FullName).IsRequired();
                component.Property(c => c.Installed).IsRequired();
                component.Property(c => c.Version).IsRequired();
                component.Property(c => c.IsSystemComponent).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Component> Components { get; set; }
    }
}