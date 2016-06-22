﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Mvc;
using Goofy.Presentation.Core.Controllers;
using Goofy.Presentation.Blog.Providers;

namespace Goofy.Presentation.Blog.Controllers
{
    [Route("api/blogAdmin")]
    public class BlogAdminMetadataController : BaseMetadataController
    {
        public BlogAdminMetadataController(IServiceProvider serviceProvider)
            : base(serviceProvider.GetRequiredService<BlogContextProvider>())
        {
        }
    }
}