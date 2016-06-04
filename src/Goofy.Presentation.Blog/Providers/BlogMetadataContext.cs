using System.Data.Entity;
using Goofy.Application.Blog.DTO;

namespace Goofy.Presentation.Blog.Providers
{
    public class BlogMetadataContext: DbContext
    {
        static BlogMetadataContext()
        {
            // Prevent attempt to initialize a database for this context
            Database.SetInitializer<BlogMetadataContext>(null);
        }

        public virtual IDbSet<ArticleItem> Plugins { get; set; }
    }
}
