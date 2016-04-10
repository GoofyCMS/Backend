
namespace Microsoft.Extensions.DependencyInjection
{
    public static class GoofyWebDataServiceCollectionExtensions
    {
        public static void AddWebGoofyData(this IServiceCollection services)
        {
            services.AddGoofyData();
        }
    }
}
