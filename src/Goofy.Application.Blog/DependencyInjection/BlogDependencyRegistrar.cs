using Goofy.Application.PluggableCore.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Goofy.Domain.Blog.Service.Adapter;
using Goofy.Infrastructure.Blog.Data;

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
            //services.AddSingleton<IBlogUnitOfWork, BlogContext>();
            //services.AddSingleton(typeof(IBlogServiceMapper<,>), typeof(BlogServiceMapper<,>));
            services.AddSingleton<BlogContext>();
            services.AddSingleton(typeof(BlogServiceMapper<,>));
        }
    }
}
