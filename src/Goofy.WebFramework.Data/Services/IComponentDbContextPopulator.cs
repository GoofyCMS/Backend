using Microsoft.Extensions.DependencyInjection;

namespace Goofy.WebFramework.Data.Services
{
    internal interface IComponentDbContextPopulator
    {
        void PopulateComponentDbContext(IServiceCollection services);
    }
}
