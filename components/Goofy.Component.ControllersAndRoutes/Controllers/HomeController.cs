using System.Linq;

using Microsoft.Extensions.OptionsModel;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace Goofy.Component.ControllersAndRoutes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ControllerAndRoutesConfiguration _config;
        private readonly IWriter _writer;
        private readonly BookContext _bookContext;

        public HomeController(IWriter writer, IOptions<ControllerAndRoutesConfiguration> config, BookContext bookContext)
        {
            _writer = writer;
            _config = config.Value;
            _bookContext = bookContext;
        }

        // GET: /hello/
        [HttpGet("hello")]
        [Authorize(Policy = "RequireViewComponent")]
        public string Index()
        {
            var message = "Hello World from a third party controller, protected by a RequireViewComponent policy.";
            _writer.Write(message);
            return string.Format("Message \"{0}\" was written on wwwroot directory. \n The value of SampleKey in the config.json file is \"{1}\"", message, _config.SampleKey);
        }

        [HttpGet("book")]
        public Book[] Book()
        {
            //return null;
            return _bookContext.Books.ToArray();
        }
    }
}
