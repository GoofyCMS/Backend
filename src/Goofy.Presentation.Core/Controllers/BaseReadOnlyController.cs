﻿using System.Linq;
using Microsoft.AspNet.Mvc;
using Breeze.ContextProvider;
using Goofy.Domain.Core.Entity;
using Goofy.Domain.Core.Service.Adapter;

namespace Goofy.Presentation.Core.Controllers
{
    public class BaseReadOnlyController<TEntity, TViewModel, TKey> : Controller
        where TEntity : BaseEntity
        where TViewModel : class
    {
        protected readonly ContextProvider Provider;

        protected BaseReadOnlyController(IServiceMapper<TEntity, TViewModel> serviceMapper, ContextProvider provider)
        {
            ServiceMapper = serviceMapper;
            Provider = provider;
        }

        public IServiceMapper<TEntity, TViewModel> ServiceMapper { get; }

        public virtual IQueryable<TViewModel> GetQuery(/*ODataQueryOptions<TViewModel> options*/)
        {
            var query = ServiceMapper.GetAll().AsQueryable();
            //if (options?.OrderBy == null)
            //{
            //    var keyProperty =
            //        typeof(TViewModel).GetProperties().Last(e => e.GetCustomAttribute(typeof(KeyAttribute)) != null);

            //    var parameter = Expression.Parameter(typeof(TViewModel), "obj");
            //    var body = Expression.Property(parameter, keyProperty.Name);
            //    var exp = Expression.Lambda<Func<TViewModel, TKey>>(body, parameter);

            //    query = query.OrderBy(exp);
            //}
            return query;
        }

        [HttpGet("")]
        public virtual IActionResult Get(/*ODataQueryOptions<TViewModel> options = null*/)
        {
            return Ok(GetQuery(/*options*/));
        }
    }
}
