using System.ComponentModel.DataAnnotations;

namespace Goofy.Application.Blog.DTO
{
    public class ArticleItem
    {
        [Key]
        public int ArticleId { get; set; }
        public string Content { get; set; }
    }
}
