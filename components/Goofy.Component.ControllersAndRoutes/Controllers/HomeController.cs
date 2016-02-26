using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.OptionsModel;
using System.Linq;

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
        public string Index()
        {
            var message = "Hello World from a third party controller";
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
