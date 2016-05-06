﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Data.Entity;
using Goofy.Domain.Core.Entity;
using Goofy.Domain.Core.Service.Data;
using Goofy.Domain.Core;

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
    public class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : BaseEntity
    {
        #region Constructor

        /// <summary>
        ///     Create a new instance of Repository
        /// </summary>
        /// <param name="unitOfWork">A unit of work for this repository</param>
        public ReadOnlyRepository(IUnitOfWork unitOfWork)
        {
            // check preconditions
            if (unitOfWork == null)
                // error
                throw new ArgumentNullException(nameof(unitOfWork));

            // set internal values
            UnitOfWork = unitOfWork;
            // create object set from unit of work
            Set = ((DbContext)UnitOfWork).Set<TEntity>();
        }

        #endregion

        /// <summary>
        ///     Gets the unit of work in this repository.
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; }

        protected DbSet<TEntity> Set { get; }

        #region IRepository<TEntity> Members

        /// <summary>
        ///     Get all elements of type <typeparamref name="TEntity" /> in repository.
        /// </summary>
        /// <param name="sql">Filters the elements in database BEFORE materialize the query.</param>
        /// <param name="parameters">Sql query parameters.</param>
        /// <returns>List of selected elements.</returns>
        public IEnumerable<TEntity> GetAll(string sql, params object[] parameters)
        {
            return UnitOfWork.SqlQuery<TEntity>(sql, parameters);
        }

        /// <summary>
        ///     Gets an element by it's entity key.
        /// </summary>
        /// <param name="entityKeyValues">Entity key values, the order the are same of order in mapping.</param>
        /// <returns>The element found, otherwise null.</returns>
        public virtual TEntity Find(params object[] entityKeyValues)
        {
            // local instance of entity
            TEntity entity = null;

            /*
                TODO: Esta es una implementación burda en memoria de lo que debería
                ser el verdadero Find que ejecute la consulta directo en la base
                de datos.
            */
            if (entityKeyValues != null && entityKeyValues.Length > 0)
            {
                // gets entity
                var entities = Set.Where(e => FilterByKeys(e, entityKeyValues)).ToArray();

                if (entities.Length > 1)
                    throw new ArgumentException("More than one entity were found.");
                else if (entities.Length == 1)
                {
                    entity = entities[0];
                    Set.Attach(entity);
                }
            }

            // return located entity or null
            return entity;
        }

        private bool FilterByKeys(TEntity entity, params object[] entityKeyValues)
        {
            var keyValues = entity.GetEntityKeys();
            for (int i = 0; i < keyValues.Length; i++)
                if (!keyValues[i].Equals(entityKeyValues[i]))
                    return false;
            return true;
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
        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
            bool noTracking = false,
            params Expression<Func<TEntity, object>>[] joinedEntities)
        {
            // create IDbSet 
            IQueryable<TEntity> query = Set;

            if (filter != null)
                query = query.Where(filter);

            if (joinedEntities != null)
                query = joinedEntities.Aggregate(query, (current, joinedEntity) => current.Include(joinedEntity));

            if (noTracking)
                query = query.AsNoTracking();

            // return not materialized query
            return query;
        }

        #endregion
    }
}