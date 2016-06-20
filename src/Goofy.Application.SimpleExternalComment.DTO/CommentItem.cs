using System.ComponentModel.DataAnnotations;

namespace Goofy.Application.SimpleExternalComment.DTO
{
    public class CommentItem
    {
        [Key]
        public int Id { get; set; }

        public int? ArticleId { get; set; }

        public string CommentText { get; set; }

        public virtual ArticleItem Article { get; set; }
    }
}
