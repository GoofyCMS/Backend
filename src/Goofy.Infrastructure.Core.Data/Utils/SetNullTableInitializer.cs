
using System.Data.Entity;

namespace Goofy.Infrastructure.Core.Data.Utils
{
    public class SetNullTableInitializer<T> where T : DbContext
    {
        public void Exec()
        {
            Database.SetInitializer<T>(null);
        }
    }
}
