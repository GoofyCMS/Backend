using System.Reflection;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Goofy.Security.Extensions;
using Goofy.Domain.Blog.Service.Data;
using Goofy.Domain.Blog.Service.Adapter;
using Goofy.Infrastructure.Blog.Data;
using Goofy.Infrastructure.Core.Data.Extensions;
using Goofy.Application.Core.Abstractions;
using Goofy.Domain.Blog.Entity;

namespace Goofy.Application.Blog.DependencyInjection
{
    public class BlogDependencyRegistrar : IDependencyRegistrar
    {
        public void ConfigureServices(IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddUnitOfWork(typeof(IBlogUnitOfWork), typeof(BlogContext));
            services.AddSingleton(typeof(IBlogServiceMapper<,>), typeof(BlogServiceMapper<,>));
            services.AddEntireCrudPermissions(typeof(Article));
        }
    }
}
