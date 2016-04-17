using System.Linq;
using System.Collections.Generic;

using Goofy.Core.Components;
using Goofy.Core.Components.Base;

using Goofy.Data.Components;

namespace Goofy.Data.WebFramework.Components
{
    public class ComponentStore : IComponentStore
    {
        public ComponentStore(ComponentContext componentContext)
        {
            ComponentContext = componentContext;
        }

        public IEnumerable<Component> Components
        {
            get
            {
                return ComponentContext.Components.ToArray();
            }
        }

        public ComponentContext ComponentContext { get; private set; }

        public void AddComponent(Component component)
        {
            ComponentContext.Add(component);
            ComponentContext.SaveChanges();
        }

        public void RemoveComponent(Component component)
        {
            ComponentContext.Remove(component);
            ComponentContext.SaveChanges();
        }

        public void UpdateComponent(Component component)
        {
            ComponentContext.Update(component);
            ComponentContext.SaveChanges();
        }
    }
}
