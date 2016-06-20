using Goofy.Application.SimpleExternalComment.DTO;
using Goofy.Domain.SimpleExternalComment.Entity;
using Goofy.Domain.SimpleExternalComment.Service.Adapter;
using Goofy.Presentation.Core.Controllers;
using Goofy.Presentation.SimpleExternalComment.Providers;
using Microsoft.AspNet.Mvc;

namespace Goofy.Presentation.SimpleExternalComment.Controllers
{
    [Route("api/simpleexternalcommentpublic/CommentItems")]
    public class CommentController : BaseController<Comment, CommentItem, int>
    {
        public CommentController(ISimpleCommentServiceMapper<Comment, CommentItem> serviceMapper, SimpleCommentContextProvider provider)
            : base(serviceMapper, provider)
        {
        }
    }
}
