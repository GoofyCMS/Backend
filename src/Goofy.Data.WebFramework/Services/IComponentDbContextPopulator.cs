using Microsoft.Extensions.DependencyInjection;

namespace Goofy.Data.WebFramework.Services
{
    internal interface IComponentDbContextPopulator
    {
        void PopulateComponentDbContext(IServiceCollection services);
    }
}
