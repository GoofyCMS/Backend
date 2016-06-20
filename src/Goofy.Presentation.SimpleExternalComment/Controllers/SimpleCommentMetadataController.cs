using Goofy.Presentation.Core.Controllers;
using Goofy.Presentation.SimpleExternalComment.Providers;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Goofy.Presentation.SimpleExternalComment.Controllers
{
    public class SimpleCommentMetadataController : BaseMetadataController
    {
        public SimpleCommentMetadataController(IServiceProvider serviceProvider)
            : base(serviceProvider.GetRequiredService<SimpleCommentContextProvider>())
        {
        }
    }
}
