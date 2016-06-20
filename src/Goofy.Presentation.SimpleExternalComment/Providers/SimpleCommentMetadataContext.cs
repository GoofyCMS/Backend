
using Goofy.Application.SimpleExternalComment.DTO;
using System.Data.Entity;

namespace Goofy.Presentation.SimpleExternalComment.Providers
{
    public class SimpleCommentMetadataContext : DbContext
    {
        static SimpleCommentMetadataContext()
        {
            // Prevent attempt to initialize a database for this context
            Database.SetInitializer<SimpleCommentMetadataContext>(null);
        }

        public virtual IDbSet<ArticleItem> ArticleItems { get; set; }
        public virtual IDbSet<CommentItem> CommentItems { get; set; }
    }
}
