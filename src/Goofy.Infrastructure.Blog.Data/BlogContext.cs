using System.Data.Entity;
using Goofy.Infrastructure.Core.Data.Service;
using Goofy.Infrastructure.Blog.Data.Configuration;
using Goofy.Domain.Blog.Service.Data;

namespace Goofy.Infrastructure.Blog.Data
{
    public class BlogContext : UnitOfWork, IBlogUnitOfWork
    {
        public BlogContext(string connectionString)
            : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ArticleConfiguration());
        }
    }
}
