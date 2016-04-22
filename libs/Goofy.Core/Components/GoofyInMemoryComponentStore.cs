using System;
using System.Collections.Generic;
using Goofy.Core.Components.Base;

namespace Goofy.Core.Components
{
    public class GoofyInMemoryComponentStore : IComponentStore
    {
        Dictionary<int, Component> _components;
        private readonly IComponentStorePersist<GoofyInMemoryComponentStore> _componentStorePersist;
        private int _currentId;

        public GoofyInMemoryComponentStore(IComponentStorePersist<GoofyInMemoryComponentStore> componentStorePersist)
        {
            _componentStorePersist = componentStorePersist;
            _components = new Dictionary<int, Component>();
            _currentId = 1;
        }

        public IEnumerable<Component> Components
        {
            get
            {
                foreach (var component in _components.Values)
                {
                    yield return component;
                }
            }
        }

        public void AddComponent(Component component)
        {
            AssignNewId(component);
            _components.Add(component.ComponentId, component);
            _componentStorePersist.PersistComponentStore(this);
        }

        public void RemoveComponent(Component component)
        {
            if (!_components.ContainsKey(component.ComponentId))
                throw new ArgumentException();
            _componentStorePersist.PersistComponentStore(this);
            _components.Remove(component.ComponentId);
        }

        public void UpdateComponent(Component component)
        {
            if (!_components.ContainsKey(component.ComponentId))
                throw new ArgumentException();
            if(_components[component.ComponentId].GlobalId != component.GlobalId)
                throw new InvalidOperationException("Can not update GlobalId for any component");
            _componentStorePersist.PersistComponentStore(this);
            _components[component.ComponentId] = component;
        }

        private void AssignNewId(Component component)
        {
            if (component.ComponentId == 0)
            {
                while (true)
                {
                    component.ComponentId = _currentId++;
                    if (!_components.ContainsKey(component.ComponentId))
                        break;
                }
            }
        }
    }
}
