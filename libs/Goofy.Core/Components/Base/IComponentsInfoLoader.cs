using System.Collections.Generic;

namespace Goofy.Core.Components.Base
{
    public interface IComponentsInfoProvider
    {
        IEnumerable<ComponentInfo> ComponentsInfo { get; }
    }
}
