using Goofy.Domain.Core.Entity;

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
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        ///     Add item into repository.
        /// </summary>
        /// <param name="item">Item to add to repository.</param>
        TEntity Add(TEntity item);

        /// <summary>
        ///     Delete item.
        /// </summary>
        /// <param name="item">Item to delete.</param>
        TEntity Remove(TEntity item);

        /// <summary>
        ///     Sets modified entity into the repository. When calling Commit() method in UnitOfWork these changes will be saved
        ///     into the storage.
        ///     <remarks>
        ///         Internally this method always calls Repository.Attach().
        ///     </remarks>
        /// </summary>
        /// <param name="item">Item with changes.</param>
        TEntity Modify(TEntity item);

        /// <summary>
        ///     Creates a new entity object.
        /// </summary>
        /// <returns>The new object with default values.</returns>
        TEntity Create();
    }
}