using Goofy.Domain.SimpleExternalComment.Service.Data;
using Goofy.Infrastructure.Core.Data.Service;
using Goofy.Infrastructure.SimpleExternalComment.Data.Configuration;
using System.Data.Entity;

namespace Goofy.Infrastructure.SimpleExternalComment.Data
{
    public class SimpleCommentContext : UnitOfWork, ISimpleCommentUnitOfWork
    {
        public SimpleCommentContext(string connectionString)
            : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SimpleCommentConfiguration());
        }
    }
}
