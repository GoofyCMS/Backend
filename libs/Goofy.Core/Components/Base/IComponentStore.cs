using System.Collections.Generic;

namespace Goofy.Core.Components.Base
{
    public interface IComponentStore
    {
        void AddComponent(Component component);

        void RemoveComponent(Component component);

        void UpdateComponent(Component component);

        IEnumerable<Component> Components { get; }
    }
}
