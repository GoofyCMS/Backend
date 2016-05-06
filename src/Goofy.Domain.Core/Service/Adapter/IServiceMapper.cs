using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Goofy.Domain.Core.Entity;

namespace Goofy.Domain.Core.Service.Adapter
{
    public interface IServiceMapper<TEntity, TViewModel> where TEntity : BaseEntity where TViewModel : class
    {
        /// <summary>
        ///     Adapter for converting entity to dto and viceversa
        /// </summary>
        ITypeAdapter TypeAdapter { get; set; }

        /// <summary>
        ///     Add item into repository.
        /// </summary>
        /// <param name="item">Item to add to repository.</param>
        TEntity Add(TViewModel item);

        /// <summary>
        ///     Delete item.
        /// </summary>
        /// <param name="item">Item to delete.</param>
        TEntity Remove(TViewModel item);

        /// <summary>
        ///     Sets modified entity into the repository. When calling Commit() method in UnitOfWork these changes will be saved
        ///     into the storage.
        ///     <remarks>
        ///         Internally this method always calls Repository.Attach().
        ///     </remarks>
        /// </summary>
        /// <param name="item">Item with changes.</param>
        TEntity Modify(TViewModel item);

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
        IEnumerable<TViewModel> GetAll(Expression<Func<TViewModel, bool>> filter = null, bool noTracking = false,
            params Expression<Func<TViewModel, object>>[] joinedEntities);

        /// <summary>
        ///     Gets an element by it's entity key.
        /// </summary>
        /// <param name="entityKeyValues">Entity key values, the order the are same of order in mapping.</param>
        /// <returns>The element found, otherwise null.</returns>
        TViewModel Find(params object[] entityKeyValues);

        /// <summary>
        ///     Creates a new entity object.
        /// </summary>
        /// <returns>The new object with default values.</returns>
        TViewModel Create();

        /// <summary>
        ///     Maps an entity to it's viewmodel.
        /// </summary>
        /// <param name="entity">Entity to map.</param>
        /// <param name="viewModel">ViewModel target.</param>
        /// <param name="reload">If true, then entity will get it's database current values.</param>
        void UpdateViewmodel(TEntity entity, TViewModel viewModel, bool reload = false);
    }
}