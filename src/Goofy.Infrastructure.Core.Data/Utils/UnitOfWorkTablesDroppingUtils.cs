
using Goofy.Domain.Core.Service.Data;
using Goofy.Infrastructure.Core.Data.Service;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Goofy.Infrastructure.Core.Data.Utils
{
    public static class UnitOfWorkTablesDroppingUtils
    {
        public static void DropTables(this IUnitOfWork unitOfwork)
        {
            if (unitOfwork is UnitOfWork)
            {
                try
                {
                    var dbContext = (DbContext)unitOfwork;
                    dbContext.Database.Delete();
                    dbContext.SaveChanges();
                }
                catch (Exception e) { }
            }
            else
            {
                throw new ArgumentException("Invalid unitOfWork Type");
            }
        }
    }
}
