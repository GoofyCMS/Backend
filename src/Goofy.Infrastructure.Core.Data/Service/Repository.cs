using System;
using System.Data.Entity;
using Goofy.Domain.Core.Entity;
using Goofy.Domain.Core.Service.Data;

namespace Goofy.Infrastructure.Core.Data.Service
{
    /// <summary>
    ///     Base interface for implement a "Repository Pattern".
    /// </summary>
    /// <remarks>
    ///     Indeed, one might think that IObjectSet is already a generic repository and therefore  would not need this item.
    ///     Using this interface allows us to ensure PI principle within our domain model.
    /// </remarks>
    /// <typeparam name="TEntity">Type of entity for this repository.</typeparam>
    public class Repository<TEntity> : ReadOnlyRepository<TEntity>, IRepository<TEntity> where TEntity : BaseEntity
    {
        #region Constructor

        /// <summary>
        ///     Create a new instance of Repository
        /// </summary>
        /// <param name="unitOfWork">A unit of work for this repository</param>
        public Repository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #endregion

        #region IRepository<TEntity> Members

        /// <summary>
        ///     Add item into repository.
        /// </summary>
        /// <param name="item">Item to add to repository.</param>
        public virtual TEntity Add(TEntity item)
        {
            // check item
            if (item == null)
                // error
                throw new ArgumentNullException("item");

            // attach object to unit of work
            return Set.Add(item);
        }

        /// <summary>
        ///     Creates a new entity object.
        /// </summary>
        /// <returns>The new object with default values.</returns>
        public virtual TEntity Create()
        {
            return Set.Create();
        }

        /// <summary>
        ///     Delete item.
        /// </summary>
        /// <param name="item">Item to delete.</param>
        public virtual TEntity Remove(TEntity item)
        {
            // check item
            if (item == null)
                // error
                throw new ArgumentNullException("item");

            // delete object from DbSet
            return Set.Remove(item);
        }

        /// <summary>
        ///     Sets modified entity into the repository. When calling Commit() method in UnitOfWork these changes will be saved
        ///     into the storage.
        ///     <remarks>
        ///         Internally this method always calls Repository.Attach().
        ///     </remarks>
        /// </summary>
        /// <param name="item">Item with changes.</param>
        public virtual TEntity Modify(TEntity item)
        {
            // check arguments
            if (item == null)
                // error
                throw new ArgumentNullException("item");

            // set object as modified
            Set.Attach(item);
            ((DbContext)UnitOfWork).Entry(item).State = EntityState.Modified;
            return item;
        }

        #endregion
    }
}