using Goofy.Domain.SimpleExternalComment.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Goofy.Infrastructure.SimpleExternalComment.Data.Configuration
{
    public class SimpleCommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public SimpleCommentConfiguration()
        {
            ToTable("SimpleCommentConfiguration");

            HasKey(t => t.Id);
        }
    }
}
