using Goofy.Domain.Blog.Entity;
using Goofy.Application.Blog;
using Goofy.Application.Blog.DTO;
using Goofy.Presentation.Blog.Providers;
using Goofy.Presentation.Core.Controllers;

namespace Goofy.Presentation.Blog.Controllers
{
    public class BlogController : BaseController<Article, ArticleItem, int>
    {
        public BlogController(BlogServiceMapper<Article, ArticleItem> serviceMapper, BlogContextProvider provider)
            : base(serviceMapper, provider)
        {
        }
    }
}
