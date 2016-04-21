﻿using Goofy.Core.Components;
using System;
using System.Linq;
using Xunit;

namespace Goofy.Core.Test.Components
{
    public class GoofyInMemoryComponentStoreTest
    {
        private readonly GoofyInMemoryComponentStore _componentStore;

        public GoofyInMemoryComponentStoreTest()
        {
            _componentStore = new GoofyInMemoryComponentStore();
        }

        [Fact]
        public void ShouldAddNewComponent()
        {
            var component = new Component { ComponentId = 1 };
            _componentStore.AddComponent(component);
            Assert.Equal(1, _componentStore.Components.Count());
            Assert.Contains(component, _componentStore.Components);
            Assert.Contains(new Component { ComponentId = 1 }, _componentStore.Components);
        }

        [Fact]
        public void ShouldThrowExceptionIfSameComponentIsAdded()
        {
            var component = new Component { ComponentId = 1 };
            _componentStore.AddComponent(component);
            Assert.ThrowsAny<Exception>(delegate () { _componentStore.AddComponent(component); });
            Assert.Equal(1, _componentStore.Components.Count());
        }


        [Fact]
        public void ShouldRemoveComponent()
        {
            var component = new Component { ComponentId = 1 };
            _componentStore.AddComponent(component);
            _componentStore.RemoveComponent(component);
            Assert.Equal(0, _componentStore.Components.Count());
        }

        [Fact]
        public void ShouldRemoveComponentByComponentNameOnly()
        {
            var componentId = 1;
            _componentStore.AddComponent(new Component { ComponentId = componentId });
            _componentStore.RemoveComponent(new Component { ComponentId = componentId });
            Assert.Equal(0, _componentStore.Components.Count());
        }

        [Fact]
        public void ShouldThrowExceptionIfTryingRemoveNonExistingComponent()
        {
            var component = new Component { ComponentId = 1 };
            _componentStore.AddComponent(component);
            var nonValidComponentToRemove = new Component { ComponentId = 2 };
            Assert.ThrowsAny<Exception>(delegate () { _componentStore.RemoveComponent(nonValidComponentToRemove); });
            Assert.Equal(1, _componentStore.Components.Count());
        }

        [Fact]
        public void ShouldUpdateComponent()
        {
            var component = new Component { ComponentId = 1, Installed = true };
            _componentStore.AddComponent(component);

            component.Installed = false;
            _componentStore.UpdateComponent(component);
            Assert.Equal(1, _componentStore.Components.Count());
            var updatedComponent = _componentStore.Components.First(c => c.ComponentId == component.ComponentId);
            Assert.Equal(false, updatedComponent.Installed);
        }

        [Fact]
        public void ShouldThrowExceptionIfTryinUpdateNonExistingComponent()
        {
            var component = new Component { ComponentId = 1 };
            _componentStore.AddComponent(component);
            var nonValidComponentToUpdate = new Component { ComponentId = 2 };
            Assert.ThrowsAny<Exception>(delegate () { _componentStore.UpdateComponent(nonValidComponentToUpdate); });
            Assert.Equal(1, _componentStore.Components.Count());
        }
    }
}
