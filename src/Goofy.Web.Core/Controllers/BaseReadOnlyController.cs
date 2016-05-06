using Microsoft.AspNet.Mvc;

using Goofy.Domain.Core.Entity;
using Goofy.Domain.Core.Service.Adapter;

namespace Goofy.Web.Core.Controllers
{
    public class BaseReadOnlyController<TEntity, TViewModel, TKey> : Controller where TEntity : BaseEntity
        where TViewModel : class
    {
        //protected readonly ContextProvider Provider;

        protected BaseReadOnlyController(IServiceMapper<TEntity, TViewModel> service/*, ContextProvider provider*/)
        {
            Service = service;
            //Provider = provider;
        }

        public IServiceMapper<TEntity, TViewModel> Service { get; }

        //public virtual IQueryable<TViewModel> GetQuery(ODataQueryOptions<TViewModel> options)
        //{
        //    var query = Service.GetAll().AsQueryable();
        //    if (options?.OrderBy == null)
        //    {
        //        var keyProperty =
        //                typeof(TViewModel).GetProperties().Last(e => e.GetCustomAttribute(typeof(KeyAttribute)) != null);

        //        var parameter = Expression.Parameter(typeof(TViewModel), "obj");
        //        var body = Expression.Property(parameter, keyProperty.Name);
        //        var exp = Expression.Lambda<Func<TViewModel, TKey>>(body, parameter);

        //        query = query.OrderBy(exp);
        //    }
        //    return query;
        //}

        //[Route("")]
        //[HttpGet]
        //public virtual IHttpActionResult Get(ODataQueryOptions<TViewModel> options = null)
        //{
        //    return Ok(GetQuery(options));
        //}
    }
}
