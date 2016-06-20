using Goofy.Domain.Blog.Entity;
using Goofy.Domain.Core.Entity;

namespace Goofy.Domain.SimpleExternalComment.Entity
{
    public class Comment : IdentityEntity
    {
        public int? ArticleId { get; set; }

        public string CommentText { get; set; }

        public virtual Article Article { get; set; }

    }
}
