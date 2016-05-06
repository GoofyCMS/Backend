using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.Entity;
//using Microsoft.Data.Entity.Internal;
using Goofy.Domain.Core.Service.Data;

namespace Goofy.Infrastructure.Core.Data.Service
{
    /// <summary>
    ///     Contract for UnitOfWork pattern.
    /// </summary>
    public abstract class UnitOfWork : DbContext, IUnitOfWork
    {
        protected string ConnectionString { get; private set; }

        public UnitOfWork(string connectionString)
            : base()
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        ///     Commit all changes made in a container, asynchronous version.
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            return await SaveChangesAsync(default(CancellationToken));
        }

        /// <summary>
        ///     Gets the TEntity repository.
        /// </summary>
        /// <typeparam name="TEntity">Represents a database's entity.</typeparam>
        /// <returns>A <see cref="IRepository{TEntity}" /> object.</returns>
        IRepository<TEntity> IUnitOfWork.Set<TEntity>()
        {
            return new Repository<TEntity>(this);
        }

        /// <summary>
        ///     Executes a sql query directly to the database.
        /// </summary>
        /// <typeparam name="TResult">The response type object.</typeparam>
        /// <param name="query">Sql query.</param>
        /// <param name="parameters">Sql query parameters.</param>
        /// <returns>A collection of <typeparamref name="TResult" /> objects.</returns>
        public IEnumerable<TResult> SqlQuery<TResult>(string query, params object[] parameters) where TResult : class
        {
            IEnumerable<TResult> result;
            result = Set<TResult>().FromSql(query, parameters);
            return result;
        }

    }
}