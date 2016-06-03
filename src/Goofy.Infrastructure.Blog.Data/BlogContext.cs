using System;
using System.Data.Entity;
using Goofy.Infrastructure.Core.Data.Service;
using Goofy.Infrastructure.Blog.Data.Configuration;

namespace Goofy.Infrastructure.Blog.Data
{
    public class BlogContext : UnitOfWork
    {
        public BlogContext(IServiceProvider services)
            : base(services)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ArticleConfiguration());
        }
    }
}
