using System.Data.Entity.ModelConfiguration;
using Goofy.Domain.Blog.Entity;

namespace Goofy.Infrastructure.Blog.Data.Configuration
{
    public class ArticleConfiguration : EntityTypeConfiguration<Article>
    {
        public ArticleConfiguration()
        {
            ToTable("Article");

            HasKey(t => t.ArticleId);

            Property(t => t.Content).IsRequired();

        }
    }
}
