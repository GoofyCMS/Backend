using Microsoft.Data.Entity;
using Goofy.Data;

namespace Goofy.Component.ControllersAndRoutes
{
    public class BookContext : GoofyObjectContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("BookTable");
                entity.Property(b => b.Title).IsRequired();
            });
            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("AuthorTable");
                entity.Property(a => a.Name).IsRequired();
            });
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    base.OnConfiguring(options);
        //}

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<Author> Authors { get; set; }
    }
}
