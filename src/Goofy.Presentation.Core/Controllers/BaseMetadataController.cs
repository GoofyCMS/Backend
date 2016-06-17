using Breeze.ContextProvider;
using Newtonsoft.Json.Linq;

using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using System.IO;

namespace Goofy.Presentation.Core.Controllers
{
    [AllowAnonymous]
    public abstract class BaseMetadataController : Controller
    {
        protected readonly ContextProvider Provider;

        protected BaseMetadataController(ContextProvider provider)
        {
            Provider = provider;
        }

        [HttpGet("metadata")]
        public virtual IActionResult Get()
        {
            string metadata;
            var directory = Path.Combine(Directory.GetCurrentDirectory(), "Metadata");
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var xmlFilePath = Path.Combine(directory, $"{GetType().Assembly.GetName()}.xml");

            if (System.IO.File.Exists(xmlFilePath))
                metadata = System.IO.File.ReadAllText(xmlFilePath);
            else
            {
                metadata = Provider.Metadata();
                System.IO.File.WriteAllText(xmlFilePath, metadata);
            }
            return Json(metadata);
        }


        /* Why this */
        [HttpPost("save")]
        public virtual SaveResult SaveChanges(JObject saveBundle)
        {
            return Provider.SaveChanges(saveBundle);
        }
    }
}