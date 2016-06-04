using AutoMapper;
using Goofy.Application.Blog.DTO;
using Goofy.Domain.Blog.Entity;

namespace Goofy.Infrastructure.Plugins.Adapter
{
    public class BlogProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Article, ArticleItem>();
            CreateMap<ArticleItem, Article>();
        }
    }
}
