using System;
using Microsoft.Extensions.DependencyInjection;
using Goofy.Domain.Core.Service.Adapter;
using Goofy.Presentation.Core.Providers;
using Goofy.Domain.Blog.Service.Data;

namespace Goofy.Presentation.Blog.Providers
{
    public class BlogContextProvider : BaseContextProvider<BlogMetadataContext>
    {
        public BlogContextProvider(IServiceProvider services, ITypeAdapterFactory typeAdapterFactory)
            : base(services, services.GetRequiredService<IBlogUnitOfWork>(), typeAdapterFactory)
        {
        }
    }
}
