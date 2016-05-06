using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Goofy.Domain.Core.Entity;
using System.Threading.Tasks;

namespace Goofy.Domain.Core.Service.Data
{
    /// <summary>
    ///     Base interface for implement a "Repository Pattern".
    /// </summary>
    /// <remarks>
    ///     Indeed, one might think that IObjectSet is already a generic repository and therefore  would not need this item.
    ///     Using this interface allows us to ensure PI principle within our domain model.
    /// </remarks>
    /// <typeparam name="TEntity">Type of entity for this repository.</typeparam>
    public interface IReadOnlyRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        ///     Get all elements of type <typeparamref name="TEntity" /> in repository.
        /// </summary>
        /// <param name="filter">Filters the elements in database BEFORE materialize the query.</param>
        /// <param name="noTracking">Indicates that the resulting objects are not tracked by EF.</param>
        /// <param name="joinedEntities">
        ///     Include these entities in the query result, otherwise these navigation fields will be
        ///     null.
        /// </param>
        /// <returns>List of selected elements.</returns>
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, bool noTracking = false,
            params Expression<Func<TEntity, object>>[] joinedEntities);

        /// <summary>
        ///     Get all elements of type <typeparamref name="TEntity" /> in repository.
        /// </summary>
        /// <param name="sql">Filters the elements in database BEFORE materialize the query.</param>
        /// <param name="parameters">Sql query parameters.</param>
        /// <returns>List of selected elements.</returns>
        IEnumerable<TEntity> GetAll(string sql, params object[] parameters);


        /// <summary>
        ///     Gets an element by it's entity key.
        /// </summary>
        /// <param name="entityKeyValues">Entity key values, the order the are same of order in mapping.</param>
        /// <returns>The element found, otherwise null.</returns>
        TEntity Find(params object[] entityKeyValues);
    }
}