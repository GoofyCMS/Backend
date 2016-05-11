using Goofy.Infrastructure.Core.Data.Configuration;
using Goofy.Infrastructure.Core.Data.Service;
using Microsoft.Extensions.OptionsModel;

namespace Goofy.Infrastructure.Core.Data.Sql.Service
{
    public abstract class SqlUnitOfWork : UnitOfWork
    {
        public SqlUnitOfWork(IOptions<DataAccessConfiguration> configurationOptions)
            : base(configurationOptions)
        {
        }
    }
}
