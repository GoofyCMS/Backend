using Goofy.Domain.Blog.Entity;
using Goofy.Application.Blog;
using Goofy.Application.Blog.DTO;
using Goofy.Presentation.Blog.Providers;
using Goofy.Presentation.Core.Controllers;
using Microsoft.AspNet.Mvc;
using Goofy.Domain.Blog.Service.Adapter;
using Goofy.Presentation.Core.Providers;

namespace Goofy.Presentation.Blog.Controllers
{
    [Route("api/article/ArticleItems")]
    public class BlogController : BaseController<Article, ArticleItem, int>
    {
        public BlogController(IBlogServiceMapper<Article, ArticleItem> serviceMapper, BlogContextProvider provider)
            : base(serviceMapper, provider)
        {
        }

        [HttpGet("hola_soy_blog_controller")]
        public string HolaSoyBlogController()
        {
            return "hola_soy_blog_controller";
        }

    }
}
