using Goofy.Domain.Core.Entity;

namespace Goofy.Domain.Blog.Entity
{
    public class Article : BaseEntity
    {
        public int ArticleId { get; set; }
        public string Content { get; set; }
    }
}
