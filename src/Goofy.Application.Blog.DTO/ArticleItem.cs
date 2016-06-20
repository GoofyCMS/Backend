using System;
using System.ComponentModel.DataAnnotations;

namespace Goofy.Application.Blog.DTO
{
    public class ArticleItem
    {
        [Key]
        public int Id { get; set; }

        [StringLength(256)]
        public string AuthorName { get; set; }

        public string Content { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
