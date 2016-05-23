using Breeze.ContextProvider;
using Newtonsoft.Json.Linq;

using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;

namespace Goofy.Web.Core.Controllers
{
    [AllowAnonymous]
    public abstract class BaseMetadataController : Controller
    {
        protected readonly ContextProvider Provider;

        protected BaseMetadataController(ContextProvider provider)
        {
            Provider = provider;
        }

        [Route("metadata")]
        [HttpGet]
        public virtual IActionResult Get()
        {
            return Json(Provider.Metadata());
        }

        /* Why this */
        [Route("save")]
        [HttpPost]
        public virtual SaveResult SaveChanges(JObject saveBundle)
        {
            return Provider.SaveChanges(saveBundle);
        }
    }
}