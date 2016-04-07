﻿using System;
using Microsoft.AspNet.Mvc;

using Goofy.Core.Components.Base;
using Goofy.Component.Auth.Models;

namespace Goofy.Component.Auth.Controllers
{
    [Route("AuthConfig")]
    public class AuthConfigController : Controller
    {
        private readonly IComponentJsonConfigProvider _componentConfigurator;

        public AuthConfigController(IComponentJsonConfigProvider componentConfigurator)
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
