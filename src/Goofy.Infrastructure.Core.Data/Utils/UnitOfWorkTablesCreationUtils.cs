using Goofy.Domain.Core.Service.Data;
using Goofy.Infrastructure.Core.Data.Service;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Goofy.Infrastructure.Core.Data.Utils
{
    public static class UnitOfWorkTablesCreationUtils
    {
        //this method violates open close principle
        public static void CreateTablesIfNotExist(this IUnitOfWork unitOfwork)
        {

            if (unitOfwork is UnitOfWork)
            {
                try
                {
                    var dbContext = (DbContext)unitOfwork;
                    var creationScript = ((IObjectContextAdapter)dbContext).ObjectContext.CreateDatabaseScript();
                    dbContext.Database.ExecuteSqlCommand(creationScript);
                    dbContext.SaveChanges();
                }
                catch
                {

                }
            }
            else
            {
                throw new ArgumentException("Invalid unitOfWork Type");
            }
        }

    }
}
