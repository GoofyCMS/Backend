﻿using System;
using Goofy.Domain.Identity.Entity;
using Goofy.Domain.Core.Service.Data;
using System.Collections.Generic;
using Goofy.Infrastructure.Core.Data.Service;
using System.Collections;

namespace Goofy.Infrastructure.Identity.Data.Service
{
    public class IdentityUnitOfWork<TUser> : IdentityDbContext<TUser>, IUnitOfWork
        where TUser : IdentityUser
    {
        public IdentityUnitOfWork(string connectionString)
            : base(connectionString)
        {

        }

        string GetConnectionString() { return ""; }

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
            return Database.SqlQuery<TResult>(query, parameters);
        }

        /// <summary>
        ///     Executes a sql query directly to the database.
        /// </summary>
        /// <param name="type">The type of the response items.</param>
        /// <param name="query">Sql query.</param>
        /// <param name="parameters">Sql query parameters.</param>
        /// <returns>A collection of <paramref name="type" /> objects.</returns>
        public IEnumerable SqlQuery(Type type, string query, params object[] parameters)
        {
            return Database.SqlQuery(type, query, parameters);
        }

    }
}