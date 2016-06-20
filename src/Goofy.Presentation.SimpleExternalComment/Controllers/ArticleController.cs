using Goofy.Application.SimpleExternalComment.DTO;
using Goofy.Domain.Blog.Entity;
using Goofy.Domain.SimpleExternalComment.Service.Adapter;
using Goofy.Presentation.Core.Controllers;
using Goofy.Presentation.SimpleExternalComment.Providers;
using Microsoft.AspNet.Mvc;

namespace Goofy.Presentation.SimpleExternalComment.Controllers
{
    [Route("api/simpleexternalcommentpublic/ArticleItems")]
    public class ArticleController : BaseReadOnlyController<Article, ArticleItem, int>
    {
        public ArticleController(ISimpleCommentServiceMapper<Article, ArticleItem> serviceMapper, SimpleCommentContextProvider provider)
            : base(serviceMapper, provider)
        {
        }

    }
}
