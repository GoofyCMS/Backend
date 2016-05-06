using Goofy.Domain.Core.Entity;
using Goofy.Domain.Core.Service.Adapter;
using Microsoft.AspNet.Mvc;


namespace Goofy.Web.Core.Controllers
{
    public class BaseController<TEntity, TViewModel, TKey> : BaseReadOnlyController<TEntity, TViewModel, TKey>
        where TEntity : BaseEntity where TViewModel : class
    {
        private readonly IServiceMapper<TEntity, TViewModel> _serviceMapper;

        public BaseController(IServiceMapper<TEntity, TViewModel> serviceMapper)
            : base(serviceMapper)
        {
            _serviceMapper = serviceMapper;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
