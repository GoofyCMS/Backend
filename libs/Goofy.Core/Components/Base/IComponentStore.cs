using System.Collections.Generic;

namespace Goofy.Core.Components.Base
{
    /*
        TODO:
            Sería bueno en algún momento hacer que todos estos métodos tuviesen su versión asíncrona.
    */
    public interface IComponentStore
    {
        void AddComponent(Component component);

        void RemoveComponent(Component component);

        void UpdateComponent(Component component);

        IEnumerable<Component> Components { get; }
    }
}
