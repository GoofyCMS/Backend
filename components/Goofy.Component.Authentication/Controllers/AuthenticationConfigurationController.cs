using System;
using Microsoft.AspNet.Mvc;
using Goofy.Core.Components.Base;
using Goofy.Component.Authentication.Models;

namespace Goofy.Component.Authentication.Controllers
{
    [Route("AuthenticationConfiguration")]
    public class AuthenticationConfigurationController : Controller
    {
        private readonly IComponentJsonConfigProvider _componentConfigurator;

        public AuthenticationConfigurationController(IComponentJsonConfigProvider componentConfigurator)
        {
            _componentConfigurator = componentConfigurator;
        }

        [Route("")]
        public IActionResult Index()
        {
            var entityTypes = new Type[] { typeof(Login), typeof(Register) };
            var config = _componentConfigurator.GetComponentJsonConfiguration(entityTypes);
            return new ObjectResult(config);
        }

    }
}
