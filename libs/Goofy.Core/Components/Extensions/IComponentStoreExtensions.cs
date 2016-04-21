using Microsoft.Extensions.DependencyInjection;
using Goofy.Core.Components.Base;
using Goofy.Extensions;

namespace Goofy.Core.Components.Extensions
{
    public static class IComponentStoreExtensions
    {
        public static void StartStore<T>(this T componentStore, IServiceCollection services) where T : IComponentStore
        {
            var storeStarterType = typeof(IComponentStoreStarter<>).MakeGenericType(new[] { componentStore.GetType() });
            try
            {
                dynamic storeStarter = services.Resolve(storeStarterType);
                storeStarter?.StartStore(componentStore);
            }
            catch
            {
            }
        }
    }
}
