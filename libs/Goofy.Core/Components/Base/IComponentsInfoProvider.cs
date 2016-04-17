using System.Collections.Generic;
using Goofy.Core.Infrastructure;

namespace Goofy.Core.Components.Base
{
    public interface IComponentsInfoProvider: IDesignTimeService
    {
        IEnumerable<ComponentInfo> ComponentsInfo { get; }
    }
}
