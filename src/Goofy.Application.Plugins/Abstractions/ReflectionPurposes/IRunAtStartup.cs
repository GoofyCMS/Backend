
namespace Goofy.Application.Plugins.Abstractions
{
    public interface IRunAtStartup : ISortableTask
    {
        void Run();
    }
}
