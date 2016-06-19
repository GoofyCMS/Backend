using Goofy.Application.ServerTime.Services;
using Microsoft.AspNet.Mvc;

namespace Goofy.Presentation.ServerTime
{
    [Route("api/ServerTime")]
    public class ServerTimeController : Controller
    {
        private readonly Service _service;

        public ServerTimeController(Service service)
        {
            _service = service;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            return new ObjectResult(new { time = _service.GetDateTime() });
        }
    }
}
