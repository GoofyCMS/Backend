using Microsoft.AspNet.Mvc;
using Breeze.ContextProvider;
using Goofy.Domain.Core.Entity;
using Goofy.Domain.Core.Service.Adapter;
using Newtonsoft.Json.Linq;

namespace Goofy.Presentation.Core.Controllers
{
    public class BaseController<TEntity, TViewModel, TKey> : BaseReadOnlyController<TEntity, TViewModel, TKey>
        where TEntity : BaseEntity where TViewModel : class
    {

        public BaseController(IServiceMapper<TEntity, TViewModel> serviceMapper, ContextProvider provider)
            : base(serviceMapper, provider)
        {
        }

        [Route("save")]
        [HttpPost]
        public virtual SaveResult SaveChanges(JObject saveBundle)
        {
            return Provider.SaveChanges(saveBundle);
        }
    }
}
