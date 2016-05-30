
namespace Goofy.Application.PluggableCore.Abstractions
{
    public interface IRunAtStartup : ISortableTask
    {
        void Run();
    }
}
