using Goofy.Domain.Core.Entity;

namespace Goofy.Domain.Blog.Entity
{
    public class Article : BaseEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
}
