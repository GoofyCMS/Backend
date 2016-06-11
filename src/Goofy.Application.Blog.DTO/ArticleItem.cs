using System.ComponentModel.DataAnnotations;

namespace Goofy.Application.Blog.DTO
{
    public class ArticleItem
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
    }
}
