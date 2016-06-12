using System;
using Microsoft.Extensions.DependencyInjection;
using Goofy.Domain.Administration.Service.Data;
using Goofy.Domain.Core.Service.Adapter;
using Goofy.Presentation.Core.Providers;

namespace Goofy.Presentation.Administration.Providers
{
    public class AdministrationContextProvider : BaseContextProvider<AdministrationMetadataContext>
    {
        public AdministrationContextProvider(IServiceProvider services, ITypeAdapterFactory typeAdapterFactory)
            : base(services, services.GetRequiredService<IAdministrationUnitOfWork>(), typeAdapterFactory)
        {
        }
    }
}
