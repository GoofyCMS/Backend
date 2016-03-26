using Microsoft.Data.Entity;
using Goofy.Core.Components;
using Goofy.Data.DataProvider.Services;

namespace Goofy.Data.WebFramework.Components
{
    public class ComponentContext : GoofyObjectContext
    {
        public ComponentContext(IDataProviderConfigurator dataProviderConfigurator) : base(dataProviderConfigurator)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
                No es necesario guardar dependencias entre componentes en la base de datos.
            */
            //modelBuilder.Entity<ComponentToComponent>(ctc =>
            //{
            //    ctc.ToTable("Goofy_ComponentToComponent");
            //    ctc.HasKey(t => new { t.FirstComponentId, t.SecondComponentId });
            //});

            modelBuilder.Entity<Component>(component =>
            {
                component.ToTable("Goofy_Component");
                component.HasKey(c => c.ComponentId);
                component.Property(c => c.Name).IsRequired();
                component.Property(c => c.Installed).IsRequired();
                component.Property(c => c.Version).IsRequired();
                component.Property(c => c.IsSystemComponent).IsRequired();
                //component.HasMany(c => c.Dependencies).WithOne().HasForeignKey(ctc => ctc.FirstComponentId);
            });

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Component> Components { get; set; }
    }
}