using Goofy.Domain.Blog.Entity;
using Goofy.Application.Blog.DTO;
using Goofy.Presentation.Blog.Providers;
using Goofy.Presentation.Core.Controllers;
using Microsoft.AspNet.Mvc;
using Goofy.Domain.Blog.Service.Adapter;
using Microsoft.AspNet.Authorization;

namespace Goofy.Presentation.Blog.Controllers
{
    [Route("api/blogadmin/ArticleItems")]
    public class ArticleAdminController : BaseController<Article, ArticleItem, int>
    {
        public ArticleAdminController(IBlogServiceMapper<Article, ArticleItem> serviceMapper, BlogContextProvider provider)
            : base(serviceMapper, provider)
        {
        }

        [Authorize(Policy = "RequireReadArticle")]
        public override IActionResult Get()
        {
            return base.Get();
        }

    }
}
