﻿using AutoMapper;
using Goofy.Application.SimpleExternalComment.DTO;
using Goofy.Domain.Blog.Entity;
using Goofy.Domain.SimpleExternalComment.Entity;

namespace Goofy.Infrastructure.SimpleExternalComment.Adapter
{
    public class SimpleExternalCommentProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Comment, CommentItem>()
                .ForMember(e => e.Article, e => e.Ignore());
            CreateMap<CommentItem, Comment>()
                .ForMember(e => e.Article, e => e.Ignore());

            CreateMap<Article, ArticleItem>();
            CreateMap<ArticleItem, Article>()
                .ForMember(e => e.Content, e => e.Ignore());
        }
    }
}
