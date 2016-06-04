using System;
using Goofy.Domain.Core.Service.Adapter;
using Goofy.Infrastructure.Blog.Data;
using Goofy.Presentation.Core.Providers;

namespace Goofy.Presentation.Blog.Providers
{
    public class BlogContextProvider: BaseContextProvider<BlogMetadataContext>
    {
        public BlogContextProvider(IServiceProvider services, ITypeAdapterFactory typeAdapterFactory)
            : base(new BlogContext(services), typeAdapterFactory)
        {
        }
    }
}
