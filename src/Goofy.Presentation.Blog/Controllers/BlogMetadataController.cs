using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Mvc;
using Goofy.Presentation.Core.Controllers;
using Goofy.Presentation.Blog.Providers;

namespace Goofy.Presentation.Blog.Controllers
{
    [Route("api/article")]
    public class BlogMetadataController : BaseMetadataController
    {
        public BlogMetadataController(IServiceProvider serviceProvider)
            : base(serviceProvider.GetRequiredService<BlogContextProvider>())
        {
        }
    }
}
