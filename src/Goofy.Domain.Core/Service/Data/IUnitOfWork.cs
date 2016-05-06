using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Goofy.Domain.Core.Entity;

namespace Goofy.Domain.Core.Service.Data
{
    /// <summary>
    ///     Contract for UnitOfWork pattern.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        ///     Commit all changes made in a container.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        ///     Commit all changes made in a container, asynchronous version.
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        ///     Gets the TEntity repository.
        /// </summary>
        /// <typeparam name="TEntity">Represents a database's entity.</typeparam>
        /// <returns>A <see cref="IRepository{TEntity}" /> object.</returns>
        IRepository<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        ///     Executes a sql query directly to the database.
        /// </summary>
        /// <typeparam name="TResult">The response type object.</typeparam>
        /// <param name="query">Sql query.</param>
        /// <param name="parameters">Sql query parameters.</param>
        /// <returns>A collection of <typeparamref name="TResult" /> objects.</returns>
        IEnumerable<TResult> SqlQuery<TResult>(string query, params object[] parameters) where TResult : class;
    }
}