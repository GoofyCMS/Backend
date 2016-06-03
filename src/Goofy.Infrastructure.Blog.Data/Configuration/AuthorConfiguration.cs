using Goofy.Domain.Blog.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Goofy.Infrastructure.Blog.Data.Configuration
{
    public class AuthorConfiguration : EntityTypeConfiguration<Author>
    {
        public AuthorConfiguration()
        {
            ToTable("AuthorTable");
            HasKey(a => a.AuthorId);
        }

    }
}
