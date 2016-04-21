using System;
using System.Collections.Generic;
using Goofy.Core.Components.Base;

namespace Goofy.Core.Components
{
    /*
        TODO: Esta solución tiene un gran problema por el momento,
        no se controla el estado de las componentes instaladas o no-instaladas.
    */
    public class GoofyInMemoryComponentStore : IComponentStore
    {
        Dictionary<int, Component> _components;
        private int _currentId;

        public GoofyInMemoryComponentStore()
        {
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
        }

        public void RemoveComponent(Component component)
        {
            if (!_components.ContainsKey(component.ComponentId))
                throw new ArgumentException();
            _components.Remove(component.ComponentId);
        }

        public void UpdateComponent(Component component)
        {
            if (!_components.ContainsKey(component.ComponentId))
                throw new ArgumentException();
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
