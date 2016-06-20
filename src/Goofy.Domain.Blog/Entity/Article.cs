using System;
using Goofy.Domain.Core.Entity;

namespace Goofy.Domain.Blog.Entity
{
    public class Article : IdentityEntity
    {
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
