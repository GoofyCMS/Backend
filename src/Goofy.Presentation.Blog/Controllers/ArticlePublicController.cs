using Goofy.Application.Blog.DTO;
using Goofy.Domain.Blog.Entity;
using Goofy.Domain.Blog.Service.Adapter;
using Goofy.Presentation.Blog.Providers;
using Goofy.Presentation.Core.Controllers;
using Microsoft.AspNet.Mvc;

namespace Goofy.Presentation.Blog.Controllers
{
    [Route("api/blogpublic/ArticleItems")]
    public class ArticlePublicController : BaseReadOnlyController<Article, ArticleItem, int>
    {
        public ArticlePublicController(IBlogServiceMapper<Article, ArticleItem> serviceMapper, BlogContextProvider provider)
            : base(serviceMapper, provider)
        {
        }
    }
}
