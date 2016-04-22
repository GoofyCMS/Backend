
namespace Goofy.Core.Components.Base
{
    public interface IComponentStorePersist<T> where T : IComponentStore
    {
        void PersistComponentStore(T componentStore);
    }
}
