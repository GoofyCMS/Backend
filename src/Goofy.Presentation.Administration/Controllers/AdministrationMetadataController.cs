using System;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Goofy.Presentation.Core.Controllers;
using Goofy.Presentation.Administration.Providers;

namespace Goofy.Presentation.Administration.Controllers
{
    [Route("api/administration")]
    public class AdministrationMetadataController : BaseMetadataController
    {
        public AdministrationMetadataController(IServiceProvider serviceProvider)
            : base(serviceProvider.GetRequiredService<AdministrationContextProvider>())
        {
        }
    }
}
