﻿using Goofy.Application.PluggableCore.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Goofy.Infrastructure.Blog.Data;
using Goofy.Infrastructure.Core.Data.Extensions;
using Goofy.Domain.Blog.Service.Data;
using Goofy.Domain.Blog.Service.Adapter;

namespace Goofy.Application.Blog.DependencyInjection
{
    public class BlogDependencyRegistrar : IDependencyRegistrar
    {
        public int Order
        {
            get
            {
                return 0;
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddUnitOfWork(typeof(IBlogUnitOfWork), typeof(BlogContext));
            services.AddSingleton(typeof(IBlogServiceMapper<,>), typeof(BlogServiceMapper<,>));
        }
    }
}
