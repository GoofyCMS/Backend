
namespace Goofy.Core.Components.Base
{
    public interface IComponentStateManager
    {
        IComponentStore ComponentStore { get; }

        void InstallComponentById(int componentId);

        void UninstallComponentById(int componentId);
    }
}
