using System;
using Microsoft.Extensions.DependencyInjection;
using Goofy.Domain.Core.Service.Adapter;
using Goofy.Domain.SimpleExternalComment.Service.Data;
using Goofy.Presentation.Core.Providers;

namespace Goofy.Presentation.SimpleExternalComment.Providers
{
    public class SimpleCommentContextProvider : BaseContextProvider<SimpleCommentMetadataContext>
    {
        public SimpleCommentContextProvider(IServiceProvider services, ITypeAdapterFactory typeAdapterFactory)
            : base(services, services.GetRequiredService<ISimpleCommentUnitOfWork>(), typeAdapterFactory)
        {
        }
    }
}
