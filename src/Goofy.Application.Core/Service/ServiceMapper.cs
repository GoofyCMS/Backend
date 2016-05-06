using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Goofy.Domain.Core;
using Goofy.Domain.Core.Entity;
using Goofy.Domain.Core.Service.Adapter;
using Goofy.Domain.Core.Service.Data;

namespace Goofy.Application.Core.Service
{
    public class ServiceMapper<TEntity, TViewModel> : IServiceMapper<TEntity, TViewModel> where TEntity : BaseEntity
        where TViewModel : class
    {
        public ServiceMapper(IUnitOfWork unitOfWork, ITypeAdapter typeAdapter, IRepository<TEntity> repository = null)
        {
            UnitOfWork = unitOfWork;
            TypeAdapter = typeAdapter;
            Repository = repository ?? unitOfWork.Set<TEntity>();
        }

        /// <summary>
        ///     Entity repository, can be set to custom or use default generic one from context.
        /// </summary>
        public IRepository<TEntity> Repository { get; }

        /// <summary>
        ///     Gets the unit of work in this repository.
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }

        #region Implementation of IServiceMapper<TEntity,TViewModel>

        /// <summary>
        ///     Adapter for converting entity to dto and viceversa
        /// </summary>
        public ITypeAdapter TypeAdapter { get; set; }

        /// <summary>
        ///     Add item into repository.
        /// </summary>
        /// <param name="item">Item to add to repository.</param>
        public virtual TEntity Add(TViewModel item)
        {
            // create a blank entity
            var entity = Repository.Create();
            // load data from DTO to the entity
            entity = TypeAdapter.Adapt(item, entity);
            // add to entity repository
            return Repository.Add(entity);
        }

        /// <summary>
        ///     Delete item.
        /// </summary>
        /// <param name="item">Item to delete.</param>
        public virtual TEntity Remove(TViewModel item)
        {
            // create a blank entity
            var keys = item.GetEntityKeys();
            var entity = Repository.Find(keys);
            // add to entity repository
            return Repository.Remove(entity);
        }

        /// <summary>
        ///     Sets modified entity into the repository. When calling Commit() method in UnitOfWork these changes will be saved
        ///     into the storage.
        ///     <remarks>
        ///         Internally this method always calls Repository.Attach().
        ///     </remarks>
        /// </summary>
        /// <param name="item">Item with changes.</param>
        public virtual TEntity Modify(TViewModel item)
        {
            // create a blank entity
            var entity = Repository.Create();
            // load data from DTO to the entity
            entity = TypeAdapter.Adapt(item, entity);
            // add to entity repository
            return Repository.Modify(entity);
        }

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
        public virtual IEnumerable<TViewModel> GetAll(Expression<Func<TViewModel, bool>> filter = null,
            bool noTracking = false, params Expression<Func<TViewModel, object>>[] joinedEntities)
        {

            /*
                TODO: fix this
            */
            //throw new NotImplementedException();

            var result = Repository.GetAll(noTracking: noTracking);

            var project = typeof(TViewModel) == typeof(TEntity)
                ? (IQueryable<TViewModel>)result
                : TypeAdapter.Project(result.AsQueryable(), joinedEntities);

            if (filter != null)
            {
                project = project.Where(filter);
            }

            return project;
        }

        /// <summary>
        ///     Gets an element by it's entity key.
        /// </summary>
        /// <param name="entityKeyValues">Entity key values, the order the are same of order in mapping.</param>
        /// <returns>The element found, otherwise null.</returns>
        public virtual TViewModel Find(params object[] entityKeyValues)
        {
            return TypeAdapter.Adapt<TEntity, TViewModel>(Repository.Find(entityKeyValues));
        }

        /// <summary>
        ///     Creates a new entity object.
        /// </summary>
        /// <returns>The new object with default values.</returns>
        public virtual TViewModel Create()
        {
            return TypeAdapter.Adapt<TEntity, TViewModel>(Repository.Create());
        }

        public virtual void UpdateViewmodel(TEntity entity, TViewModel viewModel, bool reload = false)
        {
            if (reload)
            {
                entity = Repository.Find(entity.GetEntityKeys());
            }
            if (entity != null && viewModel != null)
            {
                TypeAdapter.Adapt(entity, viewModel);
            }
        }

        #endregion
    }
}